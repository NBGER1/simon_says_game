using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Events;
using Gameplay.Player;
using Gameplay.Rivals;
using Gameplay.RuneObject;
using Infrastructure.Abstracts;
using Infrastructure.Events;
using Infrastructure.Managers;
using Infrastructure.Services;
using UnityEngine;

namespace Gameplay.Core
{
    public class GameCore : Singleton<GameCore>
    {
        #region Editor

        [SerializeField] private PopupElements _popupElements;
        [SerializeField] private GameModel _gameModel;
        [SerializeField] private RivalView _rivalView;
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private RunesElements _runesElements;
        [SerializeField] private GameObject _runesLayout;
        [SerializeField] private GameObject _canvas;
        [SerializeField] private PlayerModel _playerModel;

        #endregion

        #region Fields

        private RivalModel _rivalParams;
        private Queue<int> _lastGameSequence = new Queue<int>();

        #endregion

        #region Methods

        public void Initialize()
        {
            InitializeRunes();
            UIManager.Instance.Initialize();
            InitializePlayer();
            InitializeRival();

            GameplayServices.CoroutineService
                .WaitFor(1.5f)
                .OnEnd(StartGameSequence);
        }

        private void InitializeRival()
        {
            var rivalIndex = -1;
            if (_playerModel.LastRivalIndex > -1)
            {
                _rivalParams = RivalManager.Instance.GetRivalByIndex(_playerModel.LastRivalIndex);
            }
            else
            {
                (_rivalParams, rivalIndex) = RivalManager.Instance.GetRandomRival();
                _playerModel.SetRivalIndex(rivalIndex);
            }

            _rivalView.Initialize(_rivalParams);
        }

        private void InitializePlayer()
        {
            _playerModel.AddHealth(_playerModel.MaxHealth);
            _playerView.Initialize(_playerModel);
        }

        private IEnumerator InstantiateRune(RuneView rune)
        {
            Instantiate(rune, _runesLayout.transform);
            yield return null;
        }

        private void InitializeRunes()
        {
            for (var i = 0; i < _gameModel.RunesInScene; i++)
            {
                var rune = _runesElements.GetRuneByIndex(i);
                if (rune != null)
                {
                    GameplayServices.CoroutineService.RunCoroutine(InstantiateRune(rune));
                }
            }
        }

        private void StartGameSequence()
        {
            _lastGameSequence = _rivalView.GetNewGameSequence();
            StartRivalTurn();
            Queue<int> copyQueue = new Queue<int>(_lastGameSequence);
            var totalCount = _lastGameSequence.Count + 1;
            for (var i = 1; i < totalCount; i++)
            {
                var index = copyQueue.Dequeue();
                GameplayServices.CoroutineService
                    .WaitFor(i)
                    .OnStart(() => { GameplayServices.CoroutineService.RunCoroutine(DeselectRune(index)); })
                    .OnEnd(() =>
                    {
                        GameplayServices.CoroutineService.RunCoroutine(HighlightRunes(index));
                        GameplayServices.CoroutineService
                            .WaitFor(_gameModel.RuneSelectionDelay)
                            .OnEnd(() => { GameplayServices.CoroutineService.RunCoroutine(DeselectRune(index)); });
                    });
            }

            GameplayServices.CoroutineService
                .WaitFor(totalCount)
                .OnEnd(StartPlayerTurn);
        }

        private void OnRivalDefeat()
        {
            Debug.Log("RIVAL DEFEATED!");
            _playerModel.AddScore(_rivalParams.Score);
            var eventParams = EventParams.Empty;
            GameplayServices.EventBus.Publish(EventTypes.OnRivalDefeat, eventParams);
            GameplayServices.CoroutineService
                .WaitFor(2)
                .OnEnd(GetNextRival);
        }

        public void ResetPlayer()
        {
            _playerModel.SetRivalIndex(-1);
            _playerModel.ResetScore();
            _playerModel.ResetLives();
        }

        private void OnPlayerDeath()
        {
            Debug.Log("PLAYER DEFEATED!");
            var eventParams = EventParams.Empty;
            GameplayServices.EventBus.Publish(EventTypes.OnPlayerDeath, eventParams);
            ResetPlayer();
            if (!_playerView.hasLives())
            {
                Instantiate(_popupElements.LosePopup, _canvas.transform);
            }
            else
            {
                ResetStage();
            }
        }

        public void ResetStage()
        {
            InitializeRival();
            InitializePlayer();

            GameplayServices.CoroutineService
                .WaitFor(0.5f)
                .OnEnd(() => { StartGameSequence(); });
        }

        private void OnGameOver()
        {
            ResetPlayer();
            var eventParams = EventParams.Empty;
            GameplayServices.EventBus.Publish(EventTypes.OnGameOverWin, eventParams);
            Instantiate(_popupElements.WinPopup, _canvas.transform);
        }


        private void GetNextRival()
        {
            ResetStage();
        }

        private void StartPlayerTurn()
        {
            var eventParams = EventParams.Empty;
            GameplayServices.EventBus.Publish(EventTypes.OnPlayerTurn, eventParams);
        }

        private void StartRivalTurn()
        {
            var eventParams = EventParams.Empty;
            GameplayServices.EventBus.Publish(EventTypes.OnRivalTurn, eventParams);
        }

        IEnumerator DeselectRune(int index)
        {
            _runesLayout.transform.GetChild(index).GetComponent<RuneView>().DeselectRune();
            yield return null;
        }

        IEnumerator HighlightRunes(int index)
        {
            _runesLayout.transform.GetChild(index).GetComponent<RuneView>().HighlightRune();
            yield return null;
        }

        public void ComparePlayerSelection(int index)
        {
            var nextIndex = _lastGameSequence.Dequeue();
            if (index != nextIndex)
            {
                OnPlayerMiss(nextIndex, index);
            }
            else
            {
                OnPlayerSuccess();
            }
        }

        private void OnPlayerMiss(int correctIndex, int badIndex)
        {
            Debug.Log($"You missed with {badIndex}! The correct index was {correctIndex}");
            var eventParams = new OnDamageTaken(_rivalParams.Damage);
            GameplayServices.EventBus.Publish(EventTypes.OnPlayerSequenceFailure, eventParams);
            if (!_playerView.hasHealth())
            {
                OnPlayerDeath();
                return;
            }

            DelayedCallbacks(3, StartRivalTurn, StartGameSequence);
        }

        private void DelayedCallbacks(float delay, Action startCallback, Action endCallback)
        {
            GameplayServices.CoroutineService
                .WaitFor(delay)
                .OnStart(() => { startCallback?.Invoke(); })
                .OnEnd(() => { endCallback?.Invoke(); });
        }

        private void OnPlayerSuccess()
        {
            if (_lastGameSequence.Count == 0)
            {
                var eventParams = EventParams.Empty;
                GameplayServices.EventBus.Publish(EventTypes.OnPlayerSequenceSuccess, eventParams);
                DelayedCallbacks(3, StartRivalTurn, StartGameSequence);
            }
            else
            {
                if (!_rivalView.IsAlive())
                {
                    OnRivalDefeat();
                    return;
                }

                var eventParams = EventParams.Empty;
                DelayedCallbacks(_gameModel.RuneSelectionDelay, null,
                    () => { GameplayServices.EventBus.Publish(EventTypes.OnRuneSelectionEnd, eventParams); });
            }
        }

        protected override GameCore GetInstance()
        {
            return this;
        }

        #endregion

        #region Properties

        public GameModel GameModel => _gameModel;
        public PlayerModel PlayerModel => _playerModel;
        public GameObject Canvas => _canvas;

        #endregion
    }
}
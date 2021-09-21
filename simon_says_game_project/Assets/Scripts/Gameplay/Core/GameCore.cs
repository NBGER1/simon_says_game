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
            _rivalParams = RivalManager.Instance.GetRivalByIndex(_playerModel.Stage);
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
            if (!_rivalView.IsAlive())
            {
                OnRivalDefeat();
                return;
            }

            if (!_playerView.IsAlive())
            {
                OnPlayerDeath();
                return;
            }

            StartRivalTurn();
            Queue<int> copyQueue = new Queue<int>(_lastGameSequence);
            var totalCount = _lastGameSequence.Count;
            for (var i = 0; i < totalCount; i++)
            {
                var index = copyQueue.Dequeue();
                GameplayServices.CoroutineService
                    .WaitFor(i)
                    .OnStart(() => { GameplayServices.CoroutineService.RunCoroutine(DeselectRune(index)); })
                    .OnEnd(() =>
                    {
                        GameplayServices.CoroutineService.RunCoroutine(HighlightRunes(index));
                        GameplayServices.CoroutineService
                            .WaitFor(0.8f)
                            .OnStart(() => { Debug.Log($"I = {i}"); })
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
            _playerModel.SetStage(0);
            _playerModel.ResetScore();
        }

        private void OnPlayerDeath()
        {
            Debug.Log("PLAYER DEFEATED!");
            var eventParams = EventParams.Empty;
            GameplayServices.EventBus.Publish(EventTypes.OnPlayerDeath, eventParams);
            ResetPlayer();
            Instantiate(_popupElements.LosePopup, _canvas.transform);
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
            Instantiate(_popupElements.WinPopup, _canvas.transform);
        }

        private void GetNextRival()
        {
            var newStage = _playerModel.Stage + 1;
            if (RivalManager.Instance.GetRivalByIndex(newStage) == null)
            {
                OnGameOver();
                return;
            }

            _playerModel.SetStage(newStage);
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
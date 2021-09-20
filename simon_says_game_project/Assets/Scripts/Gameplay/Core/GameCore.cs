using System;
using System.Collections;
using System.Collections.Generic;
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

        [SerializeField] private GameModel _gameModel;
        [SerializeField] private RivalView _rivalView;
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private RunesElements _runesElements;
        [SerializeField] private GameObject _runesLayout;
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
                .OnEnd(() => { StartGameSequence(); });
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

        IEnumerator InstantiateRune(RuneView rune)
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
            StartRivalTurn();
            _lastGameSequence = _rivalView.GetNewGameSequence();
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
                OnPlayerSuccess(index);
            }
        }

        private void OnPlayerMiss(int correctIndex, int badIndex)
        {
            Debug.Log($"You missed with {badIndex}! The correct index was {correctIndex}");
            DelayedCallbacks(3, StartRivalTurn, StartGameSequence);
        }

        private void DelayedCallbacks(float delay, Action startCallback, Action endCallback)
        {
            GameplayServices.CoroutineService
                .WaitFor(delay)
                .OnStart(() => { startCallback?.Invoke(); })
                .OnEnd(() => { endCallback?.Invoke(); });
        }

        private void OnPlayerSuccess(int index)
        {
            var delay = 3;
            if (_lastGameSequence.Count == 0)
            {
                Debug.Log($"NICE! PREPARE FOR NEXT ROUND IN {delay}");
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

        #endregion
    }
}
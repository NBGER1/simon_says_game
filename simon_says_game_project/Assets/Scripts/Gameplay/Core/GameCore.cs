using System.Collections;
using System.Collections.Generic;
using Gameplay.Player;
using Gameplay.Rivals;
using Gameplay.Rune;
using Gameplay.RuneObject;
using Infrastructure.Abstracts;
using Infrastructure.Managers;
using Infrastructure.Services;
using UnityEngine;

namespace Gameplay.Core
{
    public class GameCore : Singleton<GameCore>
    {
        #region Editor

        [SerializeField] private RivalView _rivalView;
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private RunesElements _runesElements;
        [SerializeField] private GameObject _runesLayout;
        [SerializeField] private PlayerModel _playerModel;

        #endregion

        #region Fields

        private RivalModel _rivalParams;
        private Queue<int> _lastGameSequence = new Queue<int>();
        private int _maxRuneIndex;
        private int _minRuneIndex;

        #endregion

        #region Methods

        public void Initialize()
        {
            Instantiate(_runesElements.Fehu, _runesLayout.transform);
            Instantiate(_runesElements.Ehwaz, _runesLayout.transform);
            UIManager.Instance.Initialize();
            _playerModel.AddHealth(_playerModel.MaxHealth);
            _playerView.Initialize(_playerModel);
            _rivalParams = RivalManager.Instance.GetRivalByIndex(_playerModel.Stage);
            _rivalView.Initialize(_rivalParams);
            //StartNewRound();
        }

        private void StartNewRound()
        {
            Debug.Log($"Game Sequence is {_lastGameSequence}");
            StartGameSequence();
        }

        private void StartGameSequence()
        {
            Queue<int> copyQueue = new Queue<int>(_lastGameSequence);
            for (var i = 0; i < _lastGameSequence.Count; i++)
            {
                var index = copyQueue.Dequeue();
                GameplayServices.CoroutineService
                    .WaitFor(i)
                    .OnStart(() => { GameplayServices.CoroutineService.RunCoroutine(DeselectRune(index)); })
                    .OnEnd(() => { GameplayServices.CoroutineService.RunCoroutine(HighlightRunes(index)); });
                GameplayServices.CoroutineService
                    .WaitFor(i * 2 + 1)
                    .OnEnd(() => { GameplayServices.CoroutineService.RunCoroutine(DeselectRune(index)); });
            }
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
        }

        private void OnPlayerSuccess(int index)
        {
            Debug.Log($"{index} is Correct!");
            if (_lastGameSequence.Count == 0)
            {
                Debug.Log("You hit your rival!");
            }
        }

        protected override GameCore GetInstance()
        {
            return this;
        }

        #endregion

        #region Properties

        public int MaxRuneIndex => _maxRuneIndex;
        public int MinRuneIndex => _minRuneIndex;

        #endregion
    }
}
using System.Collections;
using System.Collections.Generic;
using Gameplay.Player;
using Infrastructure.Abstracts;
using Infrastructure.Managers;
using Infrastructure.Services;
using UnityEngine;

namespace Gameplay.Core
{
    public class GameCore : Singleton<GameCore>
    {
        #region Editor

        [SerializeField] private RunesElements _runesElements;
        [SerializeField] private GameObject _runesLayout;
        [SerializeField] private PlayerModel _playerModel;

        #endregion

        #region Fields

        private Queue<int> _lastGameSequence = new Queue<int>();

        #endregion

        #region Methods

        public void Initialize()
        {
            Instantiate(_runesElements.Fehu, _runesLayout.transform);
            Instantiate(_runesElements.Ehwaz, _runesLayout.transform);
            UIManager.Instance.Initialize();
            _playerModel.AddHealth(_playerModel.MaxHealth);

            GenerateNewGameSequence();
        }

        private void GenerateNewGameSequence()
        {
            var max = 2;
            var min = 0;
            var total = 3;
            for (var i = 0; i < total; i++)
            {
                _lastGameSequence.Enqueue(Random.Range(min, max));
            }

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
            _runesLayout.transform.GetChild(index).GetComponent<Rune.Base.Rune>().DeselectRune();
            yield return null;
        }

        IEnumerator HighlightRunes(int index)
        {
            _runesLayout.transform.GetChild(index).GetComponent<Rune.Base.Rune>().HighlightRune();
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
            GenerateNewGameSequence();
        }

        private void OnPlayerSuccess(int index)
        {
            Debug.Log($"{index} is Correct!");
            if (_lastGameSequence.Count == 0)
            {
                Debug.Log("You hit your rival!");
                GenerateNewGameSequence();
            }
        }

        #endregion

        protected override GameCore GetInstance()
        {
            return this;
        }
    }
}
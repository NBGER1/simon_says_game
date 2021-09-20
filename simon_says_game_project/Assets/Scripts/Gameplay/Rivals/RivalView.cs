using System.Collections.Generic;
using Gameplay.Core;
using Gameplay.Events;
using Infrastructure.Events;
using Infrastructure.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Rivals
{
    public class RivalView : MonoBehaviour
    {
        #region Editor

        [SerializeField] private RawImage _image;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private GameObject _turnIndicator;

        #endregion

        #region Fields

        private RivalModel _rivalModel;
        private float _health;

        #endregion

        #region Methods

        public void Initialize(RivalModel rivalModel)
        {
            _rivalModel = rivalModel;
            _image.texture = _rivalModel.Image;
            _name.text = _rivalModel.Name;
            _health = _rivalModel.Health;

            var eParams = new OnHealthChange(_health);
            GameplayServices.EventBus.Publish(EventTypes.OnRivalAddHealth, eParams);

            GameplayServices.EventBus.Subscribe(EventTypes.OnPlayerTurn, OnPlayerTurn);
            GameplayServices.EventBus.Subscribe(EventTypes.OnRivalTurn, OnRivalTurn);
            GameplayServices.EventBus.Subscribe(EventTypes.OnPlayerSequenceSuccess, TakeSelfDamage);
            PlayIntroAudio();
        }


        private void TakeSelfDamage(EventParams obj)
        {
            _health = Mathf.Max(_health - _rivalModel.SelfDamage, 0);
            var eParams = new OnHealthChange(_health);
            GameplayServices.EventBus.Publish(EventTypes.OnRivalTakeDamage, eParams);
        }

        private void OnRivalTurn(EventParams obj)
        {
            _turnIndicator.SetActive(true);
        }

        private void OnPlayerTurn(EventParams obj)
        {
            _turnIndicator.SetActive(false);
        }


        public Queue<int> GetNewGameSequence()
        {
            if (IsAlive())
            {
                var max = GameCore.Instance.GameModel.RunesInScene;
                var min = 0;
                var total = Random.Range(_rivalModel.MinGameSequenceLength, _rivalModel.MaxGameSequenceLength);
                Queue<int> gameSequence = new Queue<int>();
                for (var i = 0; i < total; i++)
                {
                    gameSequence.Enqueue(Random.Range(min, max));
                }

                return gameSequence;
            }

            return null;
        }

        public bool IsAlive()
        {
            return _health > 0;
        }

        public void PlayIntroAudio()
        {
            _audioSource.PlayOneShot(_rivalModel.IntroAudio);
        }

        public void PlayAttackAudio()
        {
            _audioSource.PlayOneShot(_rivalModel.AttackAudio);
        }

        public void PlayDefeatAudio()
        {
            _audioSource.PlayOneShot(_rivalModel.DefeatAudio);
        }

        #endregion
    }
}
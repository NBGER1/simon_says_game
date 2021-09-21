using Infrastructure.Abstracts;
using Infrastructure.Events;
using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.Managers
{
    public class SfxManager : Singleton<SfxManager>
    {
        #region Editor

        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private SceneSFXModel _sfxModel;

        #endregion

        #region Methods

        public void Initialize()
        {
            GameplayServices.EventBus.Subscribe(EventTypes.OnPlayerDeath, OnPlayerDeath);
            GameplayServices.EventBus.Subscribe(EventTypes.OnRivalDefeat, OnRivalDefeat);
        }

        private void OnPlayerDeath(EventParams obj)
        {
            _audioSource.clip = _sfxModel.RoundLoseClip;
            _audioSource.Play();
        }

        private void OnRivalDefeat(EventParams obj)
        {
            _audioSource.clip = _sfxModel.RoundWinClip;
            _audioSource.Play();
        }

        protected override SfxManager GetInstance()
        {
            return this;
        }

        #endregion
    }
}
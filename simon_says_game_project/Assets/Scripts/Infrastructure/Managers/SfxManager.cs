using System;
using Gameplay.Events;
using Infrastructure.Abstracts;
using Infrastructure.Events;
using Infrastructure.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.Managers
{
    public class SfxManager : Singleton<SfxManager>
    {
        #region Editor

        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private SceneSFXModel _sfxModel;

        #endregion

        #region Consts

        private const string GAME_SCENE_NAME = "Game";

        #endregion
       
        #region Methods

        private void SubscribeEvents()
        {
            GameplayServices.EventBus.Subscribe(EventTypes.OnPlayerDeath, OnPlayerDeath);
            GameplayServices.EventBus.Subscribe(EventTypes.OnRivalDefeat, OnRivalDefeat);
            GameplayServices.EventBus.Subscribe(EventTypes.OnGameOverWin, OnGameOverWin);
            GameplayServices.EventBus.Subscribe(EventTypes.OnPlayerTakeDamage, OnDamageTaken);
            GameplayServices.EventBus.Subscribe(EventTypes.OnRivalTakeDamage, OnDamageTaken);
            GameplayServices.EventBus.Subscribe(EventTypes.OnUIButtonClick, OnUIButtonClick);
        }
        

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            SubscribeEvents();

            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            UnsubscribeEvents();
            SubscribeEvents();
            return;
            if (arg0.name.Equals(GAME_SCENE_NAME))
            {
               
            }
        }

        private void OnUIButtonClick(EventParams obj)
        {
            _audioSource.clip = _sfxModel.OnUIButtonclick;
            _audioSource.Play();
        }

        private void OnDamageTaken(EventParams obj)
        {
            _audioSource.clip = _sfxModel.OnTakenDamageClip;
            _audioSource.Play();
        }

        private void OnGameOverWin(EventParams obj)
        {
            _audioSource.clip = _sfxModel.GameOverWinClip;
            _audioSource.Play();
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

        private void UnsubscribeEvents()
        {
            GameplayServices.EventBus.Unsubscribe(EventTypes.OnPlayerDeath, OnPlayerDeath);
            GameplayServices.EventBus.Unsubscribe(EventTypes.OnRivalDefeat, OnRivalDefeat);
            GameplayServices.EventBus.Unsubscribe(EventTypes.OnGameOverWin, OnGameOverWin);
            GameplayServices.EventBus.Unsubscribe(EventTypes.OnPlayerTakeDamage, OnDamageTaken);
            GameplayServices.EventBus.Unsubscribe(EventTypes.OnRivalTakeDamage, OnDamageTaken);
        }

        private void OnDestroy()
        {
            UnsubscribeEvents();
        }

        #endregion
    }
}
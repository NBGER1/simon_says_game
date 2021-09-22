using System;
using Gameplay.Events;
using Gameplay.HealthBar;
using Infrastructure.Abstracts;
using Infrastructure.Events;
using Infrastructure.Services;
using TMPro;
using UnityEngine;

namespace Infrastructure.Managers
{
    public class UIManager : Singleton<UIManager>
    {
        #region Editor

        [SerializeField] private HealthBar _playerHealthBar;
        [SerializeField] private HealthBar _rivalHealthBar;
        [SerializeField] private TextMeshProUGUI _gameTimer;

        #endregion

        #region Methods

        public void Initialize()
        {
            _playerHealthBar.Initialize();
            _rivalHealthBar.Initialize();
            GameplayServices.EventBus.Subscribe(EventTypes.OnGameTimerValueChange, OnGameTimerValueChange);
        }

        private void OnGameTimerValueChange(EventParams obj)
        {
            var eParams = obj as OnGameTimerValueChange;
            _gameTimer.text = eParams.Time.ToString();
        }

        protected override UIManager GetInstance()
        {
            return this;
        }

        private void OnDestroy()
        {
            GameplayServices.EventBus.Unsubscribe(EventTypes.OnGameTimerValueChange, OnGameTimerValueChange);

        }

        #endregion
    }
}
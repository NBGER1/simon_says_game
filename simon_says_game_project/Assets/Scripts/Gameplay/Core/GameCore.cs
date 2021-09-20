using System.Collections.Generic;
using System.Reflection;
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

        #region Methods

        public void Initialize()
        {
            Instantiate(_runesElements.Ehwaz, _runesLayout.transform);
            Instantiate(_runesElements.Fehu, _runesLayout.transform);
            UIManager.Instance.Initialize();
            _playerModel.AddHealth(_playerModel.MaxHealth);
        }

        #endregion

        protected override GameCore GetInstance()
        {
            return this;
        }
    }
}
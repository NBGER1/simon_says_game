using System.Collections.Generic;
using System.Reflection;
using Infrastructure.Abstracts;
using Infrastructure.Services;
using UnityEngine;

namespace Gameplay.Core
{
    public class GameCore : Singleton<GameCore>
    {
        #region Editor

        [SerializeField] private RunesElements _runesElements;
        [SerializeField] private GameObject _runesLayout;

        #endregion

        #region Methods

        public void Initialize()
        {
            Instantiate(_runesElements.Ehwaz, _runesLayout.transform);
            Instantiate(_runesElements.Fehu, _runesLayout.transform);
        }

        #endregion

        protected override GameCore GetInstance()
        {
            return this;
        }
    }
}
using Gameplay.HealthBar;
using Infrastructure.Abstracts;
using UnityEngine;

namespace Infrastructure.Managers
{
    public class UIManager : Singleton<UIManager>
    {
        #region Editor

        [SerializeField] private HealthBar _playerHealthBar;
        [SerializeField] private HealthBar _rivalHealthBar;

        #endregion

        #region Methods

        public void Initialize()
        {
            _playerHealthBar.Initialize();
            _rivalHealthBar.Initialize();
        }

        protected override UIManager GetInstance()
        {
            return this;
        }

        #endregion
    }
}
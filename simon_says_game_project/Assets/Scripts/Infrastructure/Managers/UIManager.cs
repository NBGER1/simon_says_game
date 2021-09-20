using Gameplay.Avatars;
using Gameplay.HealthBar;
using Gameplay.Rivals;
using Infrastructure.Abstracts;
using UnityEngine;

namespace Infrastructure.Managers
{
    public class UIManager : Singleton<UIManager>
    {
        #region Editor

        [SerializeField] private HealthBar _playerHealthBar;
        [SerializeField] private HealthBar _rivalHealthBar;

        [SerializeField] private Profile _rivalProfile;

        #endregion

        #region Methods

        public void Initialize()
        {
            _playerHealthBar.Initialize();
            _rivalHealthBar.Initialize();
        }

        public void InitializeRival(RivalParams rivalParams)
        {
            _rivalProfile.SetName(rivalParams.Name);
            _rivalProfile.SetImageTexture(rivalParams.Image);
        }

        protected override UIManager GetInstance()
        {
            return this;
        }

        #endregion
    }
}
using Gameplay.Core;

namespace Infrastructure.Services
{
    public static class GameplayServices
    {
        #region Methods

        public static void Initialize()
        {
            GameCore.Instance.Initialize();
        }

        #endregion
    }
}
using Gameplay.Core;
using Gameplay.Player;

namespace Infrastructure.Services
{
    public static class GameplayServices
    {
        #region Fields

        private static EventBus _eventBus;

        #endregion

        #region Methods

        public static void Initialize()
        {
            _eventBus = new EventBus();
            GameCore.Instance.Initialize();
        }

        #endregion

        #region Properties

        public static EventBus EventBus => _eventBus;

        #endregion
    }
}
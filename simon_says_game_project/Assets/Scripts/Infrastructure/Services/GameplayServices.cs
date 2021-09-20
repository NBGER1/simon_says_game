using Gameplay.Core;
using Infrastructure.Services.Coroutines;
using UnityEngine;

namespace Infrastructure.Services
{
    public static class GameplayServices
    {
        #region Fields

        private static EventBus _eventBus;
        private static ICoroutineService _coroutineService;

        #endregion

        #region Methods

        public static void Initialize()
        {
            _eventBus = new EventBus();
            var csgo = new GameObject("CoroutineService");
            _coroutineService = csgo.AddComponent<CoroutineService>();


            GameCore.Instance.Initialize();
        }

        #endregion

        #region Properties

        public static EventBus EventBus => _eventBus;
        public static ICoroutineService CoroutineService => _coroutineService;

        #endregion
    }
}
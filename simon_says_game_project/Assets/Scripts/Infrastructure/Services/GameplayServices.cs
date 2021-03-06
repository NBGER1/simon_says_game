using System.Collections;
using Gameplay.Core;
using Infrastructure.Managers;
using Infrastructure.Services.Coroutines;
using UnityEngine;
using SceneManager = UnityEngine.SceneManagement.SceneManager;

namespace Infrastructure.Services
{
    public static class GameplayServices
    {
        #region Fields

        private static EventBus _eventBus;
        private static ICoroutineService _coroutineService;
        private static RivalManager _rivalManager;

        #endregion

        #region Consts

        private const string GAME_SCENE_NAME = "Game";
        private const string PRE_INTRO_SCENE_NAME = "PreIntro";
        private const string INTRO_SCENE = "Intro";

        #endregion

        #region Methods

        public static void Initialize()
        {
            _eventBus = new EventBus();
            var csgo = new GameObject("CoroutineService");
            _coroutineService = csgo.AddComponent<CoroutineService>();
            if (SceneManager.GetActiveScene().name.Equals(GAME_SCENE_NAME))
            {
                GameCore.Instance.Initialize();
            }

            if (SceneManager.GetActiveScene().name.Equals(PRE_INTRO_SCENE_NAME))
            {
                CoroutineService.RunCoroutine(WaitForEventBus());
            }

            if (SceneManager.GetActiveScene().name.Equals(INTRO_SCENE))
            {
                if (_rivalManager == null)
                {
                    var rm = new GameObject("RivalManager");
                    _rivalManager = rm.AddComponent<RivalManager>();
                    _rivalManager.Initialize();
                }
            }
        }

        static IEnumerator WaitForEventBus()
        {
            while (_eventBus == null)
            {
                yield return null;
            }

            SceneManager.LoadScene(INTRO_SCENE);
        }

        #endregion

        #region Properties

        public static EventBus EventBus => _eventBus;
        public static ICoroutineService CoroutineService => _coroutineService;

        public static RivalManager RivalManager => _rivalManager;

        #endregion
    }
}
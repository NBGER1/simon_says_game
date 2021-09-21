using UnityEngine;

namespace Infrastructure.Managers
{
    public static class SceneManager
    {
        #region Methods

        public static void MoveToGameScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        }

        public static void ExitApp()
        {
            Application.Quit();
        }

        public static void MoveToMainMenuScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Intro");
        }

        #endregion
    }
}
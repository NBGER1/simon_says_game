using UnityEngine;

namespace Infrastructure.Managers
{
    public class SceneManager
    {
        #region Methods

        public void MoveToGameScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        }

        public void ExitApp()
        {
            Application.Quit();
        }

        #endregion
    }
}
using Infrastructure.Managers;
using UnityEngine;

namespace Popups
{
    public class IntroMainMenu : MonoBehaviour
    {
        #region Methods

        public void Play()
        {
            Debug.Log("PLAY");
            SceneManager.MoveToGameScene();
        }

        public void Exit()
        {
            Debug.Log("EXIT");
            SceneManager.ExitApp();
        }

        #endregion
    }
}
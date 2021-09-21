using System;
using Gameplay.Core;
using Gameplay.Scoreboard;
using Infrastructure.Managers;
using UnityEngine;

namespace Popups
{
    public class WinPopup : MonoBehaviour
    {
        #region Methods

        public void Continue()
        {
            SceneManager.MoveToMainMenuScene();
            Destroy(gameObject);
        }

        #endregion
    }
}
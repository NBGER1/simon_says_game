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
            var score = GameCore.Instance.PlayerModel.Score;
            var name = GameCore.Instance.PlayerModel.Name;
            var textureString = GameCore.Instance.PlayerModel.Image.name;
            var entry = new ScoreboardEntryParams(score, name, textureString);
            Scoreboard.AddNewEntry(entry);
            SceneManager.MoveToMainMenuScene();
            Destroy(gameObject);
        }

        #endregion
    }
}
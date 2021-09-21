using System;
using Gameplay.Core;
using Gameplay.Scoreboard;
using Infrastructure.Managers;
using UnityEngine;

namespace Popups
{
    public class WinPopup : MonoBehaviour
    {
        #region Editor

        [SerializeField] private ScoreboardUI _scoreboardUI;

        #endregion

        #region Methods

        private void OnEnable()
        {
            Initialize();
        }

        private void Initialize()
        {
            var entries = Scoreboard.GetEntryFileNames(10);
            Instantiate(_scoreboardUI, GameCore.Instance.Canvas.transform);
            foreach (var entry in entries)
            {
                var entryParams = Scoreboard.GetEntryParams(entry);
                _scoreboardUI.AddNewEntry(entryParams);
            }
        }

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
using System;
using Gameplay.Core;
using Gameplay.Scoreboard;
using Infrastructure.Database;
using Infrastructure.Managers;
using TMPro;
using UnityEngine;

namespace Popups
{
    public class WinPopup : MonoBehaviour
    {
        #region Editor

        [SerializeField] private TextMeshProUGUI _scoreValue;

        #endregion

        #region Methods

        private void OnEnable()
        {
            _scoreValue.text = PlayerData.Instance.Score.ToString();
        }

        public void Continue()
        {
            GameCore.Instance.ContinueToNewRound();
            Destroy(gameObject);
        }

        #endregion
    }
}
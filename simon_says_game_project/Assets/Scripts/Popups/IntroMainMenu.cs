using System;
using Gameplay.Player;
using Infrastructure.Managers;
using TMPro;
using UnityEngine;

namespace Popups
{
    public class IntroMainMenu : MonoBehaviour
    {
        #region Editor

        [SerializeField] private PlayerModel _playerModel;
        [SerializeField] private TextMeshProUGUI _playButtonText;
        [SerializeField] private TextMeshProUGUI _bestScoreValueText;

        #endregion

        #region Consts

        const string CONTINUE_PLAY_BUTTON_TEXT = "CONTINUE";
        const string NEW_PLAY_BUTTON_TEXT = "PLAY";

        #endregion

        #region Methods

        private void Awake()
        {
            if (_playerModel.Stage > 0)
            {
                _playButtonText.text = CONTINUE_PLAY_BUTTON_TEXT;
            }
            else
            {
                _playButtonText.text = NEW_PLAY_BUTTON_TEXT;
            }

            _bestScoreValueText.text = _playerModel.BestScore.ToString();
        }

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
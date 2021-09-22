using System;
using System.Collections.Generic;
using System.IO;
using Gameplay.Player;
using Infrastructure.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Popups
{
    public class IntroMainMenu : MonoBehaviour
    {
        #region Editor

        [SerializeField] private PlayerModel _playerModel;
        [SerializeField] private TextMeshProUGUI _playButtonText;
        [SerializeField] private TextMeshProUGUI _bestScoreValueText;
        [SerializeField] private TextMeshProUGUI _playerNameText;
        [SerializeField] private RawImage _playerImage;

        #endregion

        #region Consts

        const string CONTINUE_PLAY_BUTTON_TEXT = "CONTINUE";
        const string NEW_PLAY_BUTTON_TEXT = "PLAY";
        const string PLAYER_NAMES_PATH = @"Assets/Scripts/Gameplay/playerNames.txt";

        #endregion

        #region Fields

        private string[] _playerNames;

        #endregion

        #region Methods

        private void Awake()
        {
            _playerNames = InitializePlayerNames();
        }

        private void Start()
        {
            if (_playerModel.LastRivalIndex > -1)
            {
                _playButtonText.text = CONTINUE_PLAY_BUTTON_TEXT;
            }
            else
            {
                _playButtonText.text = NEW_PLAY_BUTTON_TEXT;
            }

            _bestScoreValueText.text = _playerModel.BestScore.ToString();

            if (_playerModel.Name.Equals(_playerModel.DefaultPlayerName))
            {
                ResetHero();
            }
            else
            {
                ShowPlayerInfo();
            }
        }

        private void ShowPlayerInfo()
        {
            _playerImage.texture = _playerModel.Image;
            _playerNameText.text = _playerModel.Name;
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

        private string[] InitializePlayerNames()
        {
            var path = PLAYER_NAMES_PATH;
            return File.ReadAllLines(path);
        }

        private string GetPlayerName()
        {
            var randomIndex = Random.Range(0, _playerNames.Length);
           return _playerNames[randomIndex];
        }

        public void ResetHero()
        {
            _playerModel.SetName(GetPlayerName());
            _playerModel.ResetScore();
            _playerModel.ResetLives();
            _playerModel.SetRivalIndex(-1);
            ShowPlayerInfo();
        }

        #endregion
    }
}
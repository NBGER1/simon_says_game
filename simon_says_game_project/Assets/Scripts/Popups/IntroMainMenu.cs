using System;
using System.Collections.Generic;
using System.IO;
using Gameplay.Player;
using Infrastructure.Database;
using Infrastructure.Events;
using Infrastructure.Managers;
using Infrastructure.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        [SerializeField] private GameObject _musicBoxPrefab;

        #endregion

        #region Consts

        const string CONTINUE_PLAY_BUTTON_TEXT = "CONTINUE";
        const string NEW_PLAY_BUTTON_TEXT = "PLAY";
        private const string DEFAULT_PLAYER_NAME = "Syymon";

        #endregion

        #region Fields

        private string[] _playerNames;
        private GameObject _musicBox;

        #endregion

        #region Methods

        private void Awake()
        {
            GameplayServices.EventBus.Subscribe(EventTypes.OnDatabaseLoad, OnDatabaseLoad);
            Database.LoadData();

            _musicBox = GameObject.FindWithTag("Music");
            if (_musicBox == null)
            {
                _musicBox = Instantiate(_musicBoxPrefab);
            }
        }

        private void OnDatabaseLoad(EventParams obj)
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

            if (!_playerModel.Name.Equals(DEFAULT_PLAYER_NAME))
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


        public void ResetHero()
        {
            _playerModel.ResetPlayer();
            _playerModel.SetName(DEFAULT_PLAYER_NAME);
            ShowPlayerInfo();
        }

        #endregion
    }
}
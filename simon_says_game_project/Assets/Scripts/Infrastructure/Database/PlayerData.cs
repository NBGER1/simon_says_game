using System;
using Infrastructure.Abstracts;
using Infrastructure.Events;
using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.Database
{
    [Serializable]
    public class PlayerData
    {
        private static readonly PlayerData instance = new PlayerData();

        #region Fields

        [SerializeField] private int _lives = 3;
        [SerializeField] private string _image = "";
        [SerializeField] private string _name = "Player";
        [SerializeField] private float _health = 0;
        [SerializeField] private int _lastRivalIndex = -1;
        [SerializeField] private int _score = 0;
        [SerializeField] private int _bestScore = 0;

        #endregion

        #region Methods

        public static PlayerData Instance
        {
            get { return instance; }
        }


        public void Set(PlayerData data)
        {
            _lives = data._lives;
            _image = data._image;
            _name = data._name;
            _health = data._health;
            _lastRivalIndex = data._lastRivalIndex;
            _score = data._score;
            _bestScore = data._bestScore;

            var eParams = EventParams.Empty;
            GameplayServices.EventBus.Publish(EventTypes.OnDatabaseLoad, eParams);
        }

        #endregion

        #region Properties

        public string Image
        {
            get => _image;
            set => _image = value;
        }

        public float Health
        {
            get => _health;
            set => _health = value;
        }

        public int Lives
        {
            get { return _lives; }
            set => _lives = value;
        }

        public int LastRivalIndex
        {
            get => _lastRivalIndex;
            set => _lastRivalIndex = value;
        }

        public int Score
        {
            get => _score;
            set => _score = value;
        }

        public int BestScore
        {
            get => _bestScore;
            set => _bestScore = value;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        #endregion
    }
}
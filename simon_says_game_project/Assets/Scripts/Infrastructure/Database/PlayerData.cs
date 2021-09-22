using System;
using Infrastructure.Abstracts;
using UnityEngine;

namespace Infrastructure.Database
{
    [Serializable]
    public class PlayerData
    {
        private static readonly PlayerData instance = new PlayerData();

        #region Fields

        [SerializeField] private float _maxHealth;
        [SerializeField] private int _maxLives;
        [SerializeField] private int _lives;
        [SerializeField] private int _maxScore;
        [SerializeField] private string _image;
        [SerializeField] private string _name;
        [SerializeField] private float _health;
        [SerializeField] private int _lastRivalIndex = -1;
        [SerializeField] private int _score;
        [SerializeField] private int _bestScore = 0;

        #endregion

        #region Methods

        public static PlayerData Instance
        {
            get { return instance; }
        }


        public void Set(PlayerData data)
        {
            _maxHealth = data._maxHealth;
            _maxLives = data._maxLives;
            _lives = data._lives;
            _image = data._image;
            _maxScore = data._maxScore;
            _name = data._name;
            _health = data._health;
            _lastRivalIndex = data._lastRivalIndex;
            _score = data._score;
            _bestScore = data._bestScore;
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
            set => _health = Mathf.Max(_health + value, _maxHealth);
        }

        public int MaxLives => _maxLives;
        public int MaxScore => _maxScore;
        public float MaxHealth => _maxHealth;

        public int Lives
        {
            get { return _lives; }
            set => _lives = value >= 0 && value <= _maxLives ? value : _lives;
        }

        public int LastRivalIndex
        {
            get => _lastRivalIndex;
            set => _lastRivalIndex = value;
        }

        public int Score
        {
            get => _score;
            set => _score = value >= 0 && value <= _maxScore ? value : _score;
        }

        public int BestScore
        {
            get => _bestScore;
            set => _bestScore = value >= 0 && value <= _maxScore ? value : _bestScore;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        #endregion
    }
}
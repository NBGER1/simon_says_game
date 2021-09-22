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

        public string Image => _image;
        public float Health => _health;
        public int MaxLives => _maxLives;
        public int MaxScore => _maxScore;
        public int Lives => _lives;
        public float MaxHealth => _maxHealth;
        public int LastRivalIndex => _lastRivalIndex;
        public int Score => _score;
        public int BestScore => _bestScore;
        public string Name => _name;

        #endregion
    }
}
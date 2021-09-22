using Gameplay.Events;
using Infrastructure.Events;
using Infrastructure.Services;
using TMPro;
using UnityEngine;

namespace Gameplay.Player
{
    [CreateAssetMenu(menuName = "Gameplay Params/Player", fileName = "PlayerParams")]
    public class PlayerModel : ScriptableObject
    {
        #region Editor

        [SerializeField] [Range(1f, 100f)] private float _maxHealth;
        [SerializeField] [Range(1, 3)] private int _maxLives;
        [SerializeField] private int _lives;
        [SerializeField] [Range(0, 10000)] private int _maxScore;
        [SerializeField] private Texture2D _image;
        [SerializeField] private string _name;
        [SerializeField] private float _health;
        [SerializeField] private int _lastRivalIndex = -1;
        [SerializeField] private int _score;
        [SerializeField] private int _bestScore = 0;

        #endregion

        #region Fields

        #endregion

        #region Methods

        public void AddHealth(float value)
        {
            _health = Mathf.Min(_health + value, _maxHealth);
            var eParams = new OnHealthChange(_health);
            GameplayServices.EventBus.Publish(EventTypes.OnPlayerAddHealth, eParams);
        }

        public void RemoveHealth(float value)
        {
            _health = Mathf.Max(_health - value, 0);
            var emptyEventParams = EventParams.Empty;
            var eParams = new OnHealthChange(_health);
            GameplayServices.EventBus.Publish(EventTypes.OnPlayerTakeDamage, eParams);
            if (_health == 0)
            {
                GameplayServices.EventBus.Publish(EventTypes.OnPlayerZeroHealth, emptyEventParams);
            }
            else
            {
                GameplayServices.EventBus.Publish(EventTypes.OnPlayerReady, emptyEventParams);
            }
        }

        public void LoseLife()
        {
            _lives = Mathf.Max(_lives - 1, 0);
            if (_lives == 0)
            {
                var eParams = EventParams.Empty;
                GameplayServices.EventBus.Publish(EventTypes.OnPlayerDeath, eParams);
            }
            else
            {
                var eventParams = EventParams.Empty;
                GameplayServices.EventBus.Publish(EventTypes.OnPlayerNewLife, eventParams);
            }
        }

        public void ResetLives()
        {
            _lives = _maxLives;
        }

        public void AddScore(int value)
        {
            _score = Mathf.Min(_score + value, _maxScore);
            SetBestScore();
            var eParams = new OnPlayerScoreChange(_score);
            GameplayServices.EventBus.Publish(EventTypes.OnPlayerScoreChange, eParams);
        }

        public void ResetScore()
        {
            SetBestScore();
            _score = 0;
            var eParams = new OnPlayerScoreChange(_score);
            GameplayServices.EventBus.Publish(EventTypes.OnPlayerScoreChange, eParams);
        }

        private void SetBestScore()
        {
            if (_score > _bestScore)
            {
                _bestScore = _score;
            }
        }

        public void SetRivalIndex(int value)
        {
            _lastRivalIndex = value;
        }

        #endregion

        #region Properties

        public Texture2D Image => _image;
        public float Health => _health;
        public int MaxLives => _maxLives;
        public int Lives => _lives;
        public float MaxHealth => _maxHealth;
        public int LastRivalIndex => _lastRivalIndex;
        public int Score => _score;
        public int BestScore => _bestScore;
        public string Name => _name;

        #endregion
    }
}
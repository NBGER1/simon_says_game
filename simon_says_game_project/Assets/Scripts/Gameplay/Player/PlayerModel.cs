using System;
using Gameplay.Events;
using Infrastructure;
using Infrastructure.Database;
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

        [SerializeField] private Texture2D _image;
        [SerializeField] [Range(0f, 100f)] private float _maxHealth;
        [SerializeField] [Range(0f, 10000f)] private float _maxScore;
        [SerializeField] [Range(0, 3)] private int _maxLives;

        #endregion

        #region Consts

        #endregion

        #region Methods

        public void ResetHealth()
        {
            PlayerData.Instance.Health = _maxHealth;
            var eParams = new OnHealthChange(_maxHealth);
            GameplayServices.EventBus?.Publish(EventTypes.OnPlayerAddHealth, eParams);
            Database.SaveData();
        }

        public void ReduceHealth(float value)
        {
            var newHealth = Mathf.Max(PlayerData.Instance.Health - value, 0);
            PlayerData.Instance.Health = newHealth;
            var emptyEventParams = EventParams.Empty;
            var eParams = new OnHealthChange(newHealth);
            GameplayServices.EventBus?.Publish(EventTypes.OnPlayerTakeDamage, eParams);
            if (PlayerData.Instance.Health == 0)
            {
                GameplayServices.EventBus?.Publish(EventTypes.OnPlayerZeroHealth, emptyEventParams);
            }
            else
            {
                GameplayServices.EventBus?.Publish(EventTypes.OnPlayerReady, emptyEventParams);
            }
        }

        public void ReduceLives(int value)
        {
            var newLives = Mathf.Max(PlayerData.Instance.Lives - value, 0);
            PlayerData.Instance.Lives = newLives;
            if (newLives == 0)
            {
                var eParams = EventParams.Empty;
                GameplayServices.EventBus?.Publish(EventTypes.OnPlayerDeath, eParams);
            }
            else
            {
                var eventParams = EventParams.Empty;
                GameplayServices.EventBus?.Publish(EventTypes.OnPlayerNewLife, eventParams);
            }

            Database.SaveData();
        }

        public void ResetLives()
        {
            PlayerData.Instance.Lives = _maxLives;
            Database.SaveData();
        }

        public void AddScore(int value)
        {
            PlayerData.Instance.Score = value >= 0 && value <= _maxScore ? value : PlayerData.Instance.Score;
            SetBestScore();
            var eParams = new OnPlayerScoreChange(value);
            GameplayServices.EventBus?.Publish(EventTypes.OnPlayerScoreChange, eParams);
            Database.SaveData();
        }

        public void ResetScore()
        {
            SetBestScore();
            PlayerData.Instance.Score = 0;
            var eParams = new OnPlayerScoreChange(0);
            GameplayServices.EventBus?.Publish(EventTypes.OnPlayerScoreChange, eParams);
            Database.SaveData();
        }

        private void SetBestScore()
        {
            if (PlayerData.Instance.Score > PlayerData.Instance.BestScore)
            {
                PlayerData.Instance.BestScore = PlayerData.Instance.Score;
                Database.SaveData();
            }
        }

        public void SetRivalIndex(int value)
        {
            PlayerData.Instance.LastRivalIndex = value;
            Database.SaveData();
        }

        public void SetName(string value)
        {
            PlayerData.Instance.Name = value;
            Database.SaveData();
        }

        public void ResetPlayer()
        {
            ResetScore();
            ResetHealth();
            ResetLives();
            SetRivalIndex(-1);
        }

        #endregion

        #region Properties

        public Texture2D Image => _image;
        public int MaxLives => _maxLives;
        public int Lives => PlayerData.Instance.Lives;
        public float MaxHealth => _maxHealth;
        public int LastRivalIndex => PlayerData.Instance.LastRivalIndex;
        public int Score => PlayerData.Instance.Score;
        public int BestScore => PlayerData.Instance.BestScore;
        public string Name => PlayerData.Instance.Name;

        #endregion
    }
}
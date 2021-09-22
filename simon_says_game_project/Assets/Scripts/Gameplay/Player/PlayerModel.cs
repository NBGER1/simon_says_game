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

        #endregion

        #region Fields

        #endregion

        #region Methods

        private void Awake()
        {
            Database.LoadData();
        }

        public void ResetHealth()
        {
            PlayerData.Instance.Health = PlayerData.Instance.MaxHealth;
            var eParams = new OnHealthChange(PlayerData.Instance.MaxHealth);
            GameplayServices.EventBus?.Publish(EventTypes.OnPlayerAddHealth, eParams);
            Database.SaveData();
        }

        public void ReduceHealth(float value)
        {
            var newHealth = PlayerData.Instance.Health - value;
            PlayerData.Instance.Health = newHealth;
            var emptyEventParams = EventParams.Empty;
            var eParams = new OnHealthChange(newHealth);
            GameplayServices.EventBus.Publish(EventTypes.OnPlayerTakeDamage, eParams);
            if (newHealth == 0)
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
            var newLives = PlayerData.Instance.Lives - value;
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
            PlayerData.Instance.Lives = PlayerData.Instance.MaxLives;
            Database.SaveData();
        }

        public void AddScore(int value)
        {
            PlayerData.Instance.Score = value;
            SetBestScore();
            var eParams = new OnPlayerScoreChange(value);
            GameplayServices.EventBus?.Publish(EventTypes.OnPlayerScoreChange, eParams);
            Database.SaveData();
        }

        public void ResetScore()
        {
            PlayerData.Instance.Score = 0;
            SetBestScore();
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

        #endregion

        #region Properties

        public Texture2D Image => _image;
        public int MaxLives => PlayerData.Instance.MaxLives;
        public int Lives => PlayerData.Instance.Lives;
        public float MaxHealth => PlayerData.Instance.MaxHealth;
        public int LastRivalIndex => PlayerData.Instance.LastRivalIndex;
        public int Score => PlayerData.Instance.Score;
        public int BestScore => PlayerData.Instance.BestScore;
        public string Name => PlayerData.Instance.Name;

        #endregion
    }
}
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
        [SerializeField] [Range(0, 1000)] private int _maxScore;
        [SerializeField] private Texture2D _image;
        [SerializeField] private float _health;
        [SerializeField] private int _stage;
        [SerializeField] private int _score;

        #endregion

        #region Methods

        public void AddHealth(float value)
        {
            _health = Mathf.Min(_health + value, _maxHealth);
            var eParams = new OnHealthBarChange(_health);
            GameplayServices.EventBus.Publish(EventTypes.OnPlayerAddHealth, eParams);
        }

        public void RemoveHealth(float value)
        {
            _health = Mathf.Max(_health - value, 0);
            var eParams = new OnHealthBarChange(_health);
            GameplayServices.EventBus.Publish(EventTypes.OnPlayerTakeDamage, eParams);
        }

        public void AddScore(int value)
        {
            _score = Mathf.Min(_score + value, _maxScore);
        }

        public void ResetScore()
        {
            _score = 0;
        }

        public void SetStage(int value)
        {
            _stage = value;
        }

        #endregion

        #region Properties

        public Texture2D Image => _image;
        public float Health => _health;
        public float MaxHealth => _maxHealth;
        public int Stage => _stage;
        public int Score => _score;

        #endregion
    }
}
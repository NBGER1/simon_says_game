using Gameplay.Events;
using Infrastructure.Events;
using Infrastructure.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Player
{
    public class PlayerView : MonoBehaviour
    {
        #region Editor

        [SerializeField] private RawImage _image;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private GameObject _turnIndicator;
        [SerializeField] private TextMeshProUGUI _score;
        [SerializeField] private Color32 _damageColor;
        [SerializeField] private Color32 _defaultColor;
        [SerializeField] private GameObject[] _playerLivesSprites;

        #endregion

        #region Fields

        private PlayerModel _playerModel;

        #endregion

        #region Methods

        public void Initialize(PlayerModel playerModel)
        {
            _playerModel = playerModel;
            _image.texture = playerModel.Image;
            _name.text = _playerModel.Name;
            _score.text = _playerModel.Score.ToString();
            _turnIndicator.SetActive(false);
            for (var i = 0; i < _playerModel.MaxLives; i++)
            {
                if (i + 1 <= _playerModel.Lives)
                {
                    _playerLivesSprites[i].SetActive(true);
                }
                else
                {
                    _playerLivesSprites[i].SetActive(false);
                }
            }

            GameplayServices.EventBus.Subscribe(EventTypes.OnPlayerTurn, OnPlayerTurn);
            GameplayServices.EventBus.Subscribe(EventTypes.OnRivalTurn, OnRivalTurn);
            GameplayServices.EventBus.Subscribe(EventTypes.OnPlayerSequenceFailure, TakeDamage);
            GameplayServices.EventBus.Subscribe(EventTypes.OnPlayerScoreChange, OnPlayerScoreChange);
            GameplayServices.EventBus.Subscribe(EventTypes.OnPlayerDeath, OnPlayerDeath);
        }

        private void OnPlayerDeath(EventParams obj)
        {
            _playerModel.LoseLife();
        }


        private void TakeDamage(EventParams obj)
        {
            var eParams = obj as OnDamageTaken;
            _playerModel.RemoveHealth(eParams.Damage);
            TakeDamageEffect();
        }

        private void TakeDamageEffect()
        {
            GameplayServices.CoroutineService
                .WaitFor(0.1f)
                .OnStart(() => { _image.color = _damageColor; })
                .OnEnd(() => { _image.color = _defaultColor; });
        }

        private void OnRivalTurn(EventParams obj)
        {
            _turnIndicator.SetActive(false);
        }

        public bool hasLives()
        {
            return _playerModel.Lives > 0;
        }

        public bool hasHealth()
        {
            return _playerModel.Health > 0;
        }

        private void OnPlayerTurn(EventParams obj)
        {
            _turnIndicator.SetActive(true);
        }

        private void OnPlayerScoreChange(EventParams obj)
        {
            var eParams = obj as OnPlayerScoreChange;
            _score.text = eParams.Score.ToString();
        }

        #endregion
    }
}
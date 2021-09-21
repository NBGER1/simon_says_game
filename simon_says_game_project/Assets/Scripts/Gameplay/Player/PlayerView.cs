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
            GameplayServices.EventBus.Subscribe(EventTypes.OnPlayerTurn, OnPlayerTurn);
            GameplayServices.EventBus.Subscribe(EventTypes.OnRivalTurn, OnRivalTurn);
            GameplayServices.EventBus.Subscribe(EventTypes.OnPlayerSequenceFailure, TakeDamage);
            GameplayServices.EventBus.Subscribe(EventTypes.OnPlayerScoreChange, OnPlayerScoreChange);
        }


        private void TakeDamage(EventParams obj)
        {
            var eParams = obj as OnDamageTaken;
            _playerModel.RemoveHealth(eParams.Damage);
        }

        private void OnRivalTurn(EventParams obj)
        {
            _turnIndicator.SetActive(false);
        }

        public bool IsAlive()
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
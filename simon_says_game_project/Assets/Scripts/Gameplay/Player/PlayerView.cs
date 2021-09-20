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
            GameplayServices.EventBus.Subscribe(EventTypes.OnPlayerTurn, OnPlayerTurn);
            GameplayServices.EventBus.Subscribe(EventTypes.OnRivalTurn, OnRivalTurn);
        }

        private void OnRivalTurn(EventParams obj)
        {
            _turnIndicator.SetActive(false);
        }

        private void OnPlayerTurn(EventParams obj)
        {
            _turnIndicator.SetActive(true);
        }

        #endregion
    }
}
using Gameplay.Core;
using Infrastructure.Events;
using Infrastructure.Services;
using TMPro;
using UnityEngine;

namespace Popups
{
    public class WinPopup : MonoBehaviour
    {
        #region Editor

        [SerializeField] private TextMeshProUGUI _scoreValue;

        #endregion

        #region Methods

        private void OnEnable()
        {
            _scoreValue.text = GameCore.Instance.LastScoreGained.ToString();
        }

        public void Continue()
        {
            var eParams = EventParams.Empty;
            GameplayServices.EventBus.Publish(EventTypes.OnUIButtonClick, eParams);
            GameCore.Instance.ContinueToNewRound();
            Destroy(gameObject);
        }

        #endregion
    }
}
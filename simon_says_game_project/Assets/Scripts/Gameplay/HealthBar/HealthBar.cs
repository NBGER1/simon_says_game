using Infrastructure.Events;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.HealthBar
{
    public abstract class HealthBar : MonoBehaviour
    {
        #region Editor

        [SerializeField] protected Slider _slider;

        #endregion

        #region Methods

        public abstract void Initialize();
        protected abstract void SubscribeEvents();
        protected abstract void OnZeroValue();
        protected abstract void SetSliderValue(EventParams eventParams);

        #endregion
    }
}
using System;
using Gameplay.Events;
using Infrastructure.Events;
using Infrastructure.Services;
using UnityEngine;

namespace Gameplay.HealthBar
{
    public class RivalHealthBar : HealthBar
    {
        #region Methods

        public override void Initialize()
        {
            SubscribeEvents();
        }

        protected override void SubscribeEvents()
        {
            GameplayServices.EventBus.Subscribe(EventTypes.OnRivalTakeDamage, SetSliderValue);
            GameplayServices.EventBus.Subscribe(EventTypes.OnRivalAddHealth, SetSliderValue);
        }

        protected override void SetSliderValue(EventParams eventParams)
        {
            var eParams = eventParams as OnHealthBarChange;
            _slider.value = Mathf.Max(eParams.Health, _slider.maxValue);
        }


        protected override void OnZeroValue()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
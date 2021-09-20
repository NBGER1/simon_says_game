using System;
using Gameplay.Events;
using Infrastructure.Events;
using Infrastructure.Services;
using UnityEngine;

namespace Gameplay.HealthBar
{
    public class PlayerHealthBar : HealthBar
    {
        #region Methods

        public override void Initialize()
        {
            SubscribeEvents();
        }

        protected override void SubscribeEvents()
        {
            GameplayServices.EventBus.Subscribe(EventTypes.OnPlayerAddHealth, SetSliderValue);
            GameplayServices.EventBus.Subscribe(EventTypes.OnPlayerTakeDamage, SetSliderValue);
        }

        protected override void SetSliderValue(EventParams eventParams)
        {
            var eParams = eventParams as OnHealthChange;
            _slider.value = eParams.Health;
        }


        protected override void OnZeroValue()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
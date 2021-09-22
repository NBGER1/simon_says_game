using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Events;
using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.Services
{
    public class EventBus
    {
        #region Fields

        private Dictionary<EventTypes, List<Action<EventParams>>> _subscribers =
            new Dictionary<EventTypes, List<Action<EventParams>>>();

        #endregion

        #region Methods

        public void Subscribe(EventTypes eventType, Action<EventParams> callback)
        {
            if (!_subscribers.ContainsKey(eventType))
            {
                _subscribers.Add(eventType, new List<Action<EventParams>>());
            }
            _subscribers[eventType].Add(callback);
        }

        public void Unsubscribe(EventTypes eventType, Action<EventParams> callback)
        {
            if (!_subscribers.ContainsKey(eventType))
            {
                throw new NullReferenceException($"{eventType} event can't be unsubscribed");
            }

            _subscribers[eventType].Remove(callback);
        }

        public void Publish(EventTypes eventType, EventParams eventParams)
        {
           
            if (!_subscribers.ContainsKey(eventType))
            {
                return;
                throw new NullReferenceException($"{eventType} event can't be published");
            }

            foreach (var action in _subscribers[eventType].ToList())
            {
                action?.Invoke(eventParams);
            }
        }

        #endregion
    }
}
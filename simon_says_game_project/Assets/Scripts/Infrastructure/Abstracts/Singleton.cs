using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Infrastructure.Abstracts
{
    public abstract class Singleton<T> : MonoBehaviour
    {
        private static T _instance;

        #region Methods

        private void Awake()
        {
            _instance = GetInstance();
        }

        protected abstract T GetInstance();

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    throw new NullReferenceException("Singleton instance is null");
                }

                return _instance;
            }
        }

        #endregion
    }
}
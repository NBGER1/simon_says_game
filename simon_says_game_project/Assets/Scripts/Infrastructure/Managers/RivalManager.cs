using System;
using System.Linq;
using Gameplay.Rivals;
using Infrastructure.Abstracts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Infrastructure.Managers
{
    public class RivalManager : Singleton<RivalManager>
    {
        #region Fields

        private RivalModel[] _rivals;

        #endregion


        #region Methods

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            if (_rivals?.Length >= 1) return;
            _rivals = Resources.LoadAll("RivalModels", typeof(RivalModel)).Cast<RivalModel>().ToArray();
        }

        protected override RivalManager GetInstance()
        {
            return this;
        }

        public (RivalModel, int) GetRandomRival()
        {
            var randomIndex = Random.Range(0, _rivals.Length);
            Debug.Log($"RandomIndex = {randomIndex} , ArrayLength {_rivals.Length}");

            return (_rivals[randomIndex], randomIndex);
        }

        public RivalModel GetRivalByIndex(int index)
        {
            if (index < _rivals.Length)
            {
                return _rivals[index];
            }

            return null;
        }

        #endregion
    }
}
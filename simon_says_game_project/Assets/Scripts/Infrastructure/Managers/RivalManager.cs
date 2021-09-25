using System;
using System.Linq;
using Gameplay.Rivals;
using Infrastructure.Abstracts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Infrastructure.Managers
{
    public class RivalManager :MonoBehaviour
    {
        #region Fields

        private RivalModel[] _rivals;

        #endregion


        #region Methods

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void Initialize()
        {
            if (_rivals?.Length >= 1) return;
            _rivals = Resources.LoadAll("RivalModels", typeof(RivalModel)).Cast<RivalModel>().ToArray();
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
using Gameplay.Core;
using Gameplay.Rivals;
using Infrastructure.Abstracts;
using UnityEngine;

namespace Infrastructure.Managers
{
    public class RivalManager : Singleton<RivalManager>
    {
        #region Editor

        [SerializeField] private RivalModel[] _rivals;

        #endregion


        #region Methods

        protected override RivalManager GetInstance()
        {
            return this;
        }

        public (RivalModel,int) GetRandomRival()
        {
            var randomIndex = Random.Range(0, _rivals.Length);
            return (_rivals[randomIndex],randomIndex);
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
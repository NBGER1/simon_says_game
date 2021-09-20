using Gameplay.Rivals;
using Infrastructure.Abstracts;
using UnityEngine;

namespace Infrastructure.Managers
{
    public class RivalManager : Singleton<RivalManager>
    {
        #region Editor

        [SerializeField] private RivalParams[] _rivals;

        #endregion


        #region Methods

        protected override RivalManager GetInstance()
        {
            return this;
        }

        public RivalParams GetRivalByIndex(int index)
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
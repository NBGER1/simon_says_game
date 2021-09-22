using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure
{
    public class AppEntry : MonoBehaviour
    {
        #region Fields

        #endregion

        #region Methods

        private void Awake()
        {
            
        }

        void Start()
        {
            GameplayServices.Initialize();
        }

        #endregion
    }
}
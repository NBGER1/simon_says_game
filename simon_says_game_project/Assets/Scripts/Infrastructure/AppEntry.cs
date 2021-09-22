using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure
{
    public class AppEntry : MonoBehaviour
    {
        #region Fields

        #endregion

        #region Methods

        void Awake()
        {
            GameplayServices.Initialize();
        }

        #endregion
    }
}
using System;
using System.Collections;
using UnityEngine;

namespace Infrastructure.Services.Coroutines
{
    public interface ICoroutineService
    {
        #region Methods

        Coroutine RunCoroutine(IEnumerator coroutine);
        void EndCoroutine(IEnumerator coroutine);
        IAwaiter WaitFor(float delay);
        void StopAll();

        #endregion
    }
}
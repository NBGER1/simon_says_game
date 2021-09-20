using System;
using System.Collections;
using UnityEngine;

namespace Infrastructure.Services.Coroutines
{
    public class CoroutineService : MonoBehaviour, ICoroutineService
    {
        #region Methods

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public Coroutine RunCoroutine(IEnumerator coroutine)
        {
            return StartCoroutine(coroutine);
        }

        public IAwaiter WaitFor(float delay)
        {
            var awaiter = new Awaiter();
            RunCoroutine(WaitForInternal(delay, awaiter));
            return awaiter;
        }

        private IEnumerator WaitForInternal(float delay, IAwaiter awaiter)
        {
            yield return null;
            awaiter.Start();
            yield return new WaitForSeconds(delay);
            awaiter.End();
        }

        #endregion
    }
}
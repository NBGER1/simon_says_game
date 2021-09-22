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

        public void EndCoroutine(IEnumerator coroutine)
        {
            StopCoroutine(coroutine);
        }
        public IAwaiter WaitFor(float delay)
        {
            var awaiter = new Awaiter();

            RunCoroutine(WaitForInternal(delay, awaiter));
            return awaiter;
        }

        public void StopAll()
        {
            StopAllCoroutines();
        }


        private IEnumerator WaitForInternal(float delay, IAwaiter awaiter)
        {
            yield return null;
            awaiter.Start();
            var timeIteration = delay % 1 > 0 ? delay % 1 : 1;
            for (var i = 0; i < delay; i++)
            {
                awaiter.Progress(i);
                yield return new WaitForSeconds(timeIteration);
            }

            awaiter.End();
        }

        #endregion
    }
}
using System;

namespace Infrastructure.Services.Coroutines
{
    public class Awaiter : IAwaiter
    {
        #region Fields

        private Action _onStartCallback;
        private Action _onEndCallback;

        #endregion

        #region Methods

        public void OnStart(Action callback)
        {
            _onStartCallback = callback;
        }

        public void OnEnd(Action callback)
        {
            _onEndCallback = callback;
        }

        public void End()
        {
            _onEndCallback?.Invoke();
        }

        public void Start()
        {
            _onStartCallback?.Invoke();
        }

        #endregion
    }
}
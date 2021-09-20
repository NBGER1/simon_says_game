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

        public IAwaiter OnStart(Action callback)
        {
            _onStartCallback = callback;
            return this;
        }

        public IAwaiter OnEnd(Action callback)
        {
            _onEndCallback = callback;
            return this;
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
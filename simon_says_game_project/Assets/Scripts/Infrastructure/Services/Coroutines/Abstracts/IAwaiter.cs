using System;

namespace Infrastructure.Services.Coroutines
{
    public interface IAwaiter
    {
        #region Methods

        IAwaiter OnStart(Action callback);
        IAwaiter OnEnd(Action callback);
        void Start();
        void End();

        #endregion
    }
}
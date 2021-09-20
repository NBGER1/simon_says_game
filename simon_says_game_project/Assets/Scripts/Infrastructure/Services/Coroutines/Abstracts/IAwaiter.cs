using System;

namespace Infrastructure.Services.Coroutines
{
    public interface IAwaiter
    {
        #region Methods

        void OnStart(Action callback);
        void OnEnd(Action callback);
        void Start();
        void End();

        #endregion
    }
}
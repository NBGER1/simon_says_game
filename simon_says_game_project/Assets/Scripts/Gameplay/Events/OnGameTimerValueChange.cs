using Infrastructure.Events;

namespace Gameplay.Events
{
    public class OnGameTimerValueChange:EventParams
    {
        #region Fields

        private float _time;

        #endregion

        #region Constructor

        public OnGameTimerValueChange(int time)
        {
            _time = time;
        }

        #endregion

        #region Properties

        public float Time => _time;

        #endregion
    }
}
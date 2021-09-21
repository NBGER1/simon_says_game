using Infrastructure.Events;

namespace Gameplay.Events
{
    public class OnPlayerScoreChange : EventParams
    {
        #region Fields

        private float _score;

        #endregion

        #region Constructor

        public OnPlayerScoreChange(int score)
        {
            _score = score;
        }

        #endregion

        #region Properties

        public float Score => _score;

        #endregion
    }
}
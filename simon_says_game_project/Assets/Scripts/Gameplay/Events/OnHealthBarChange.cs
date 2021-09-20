using Infrastructure.Events;

namespace Gameplay.Events
{
    public class OnHealthBarChange : EventParams
    {
        #region Fields

        private float _health;

        #endregion

        #region Constructor

        public OnHealthBarChange(float health)
        {
            _health = health;
        }

        #endregion

        #region Properties

        public float Health => _health;

        #endregion
    }
}
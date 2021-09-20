using Infrastructure.Events;

namespace Gameplay.Events
{
    public class OnHealthChange : EventParams
    {
        #region Fields

        private float _health;

        #endregion

        #region Constructor

        public OnHealthChange(float health)
        {
            _health = health;
        }

        #endregion

        #region Properties

        public float Health => _health;

        #endregion
    }
}
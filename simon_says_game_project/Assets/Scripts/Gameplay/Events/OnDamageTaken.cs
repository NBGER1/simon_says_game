using Infrastructure.Events;

namespace Gameplay.Events
{
    public class OnDamageTaken:EventParams
    {
        #region Fields

        private float _damage;

        #endregion

        #region Constructor

        public OnDamageTaken(float damage)
        {
            _damage = damage;
        }

        #endregion

        #region Properties

        public float Damage => _damage;

        #endregion
    }
}
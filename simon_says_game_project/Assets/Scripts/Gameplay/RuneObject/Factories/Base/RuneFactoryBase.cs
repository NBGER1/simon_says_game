using UnityEngine;

namespace Gameplay.Rune.Factories.Base
{
    public abstract class RuneFactoryBase : MonoBehaviour
    {
        #region Editor

        [SerializeField] private GameObject _prefab;

        #endregion

        #region Methods

        public virtual Rune.Base.Rune Create()
        {
            var variant = Instantiate(_prefab);
            return Adjust(variant);
        }

        public abstract Rune.Base.Rune Adjust(GameObject gameObject);

        #endregion
    }
}
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Rune.Base
{
    public abstract class Rune : MonoBehaviour
    {
        #region Editor

        [SerializeField] protected RawImage _image;

        #endregion

        #region Fields

        public abstract void Initialize(RuneParams runeParams);
        public abstract void SelectRune();
        public abstract void DeselectRune();
        public abstract void PlayAudio();

        #endregion
    }
}
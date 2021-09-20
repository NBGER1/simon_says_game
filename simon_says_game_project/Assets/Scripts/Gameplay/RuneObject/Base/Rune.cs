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

        public abstract void Initialize();
        public abstract void SelectRune();
        public abstract void HighlightRune();
        public abstract void DeselectRune();
        public abstract void PlayAudio();
        #endregion
    }
}
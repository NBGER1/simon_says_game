using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Rivals
{
    public abstract class Rival : MonoBehaviour
    {
        #region Editor

        [SerializeField] protected RawImage _image;
        [SerializeField] protected RivalParams _params;


        #endregion
        #region Methods

        public abstract Queue<int> GetNewGameSequence();
        public abstract void Initialize();
        public abstract void PlayIntroAudio();
        public abstract void PlayAttackAudio();
        public abstract void PlayDefeatAudio();

        #endregion
    }
}
using System.Collections.Generic;
using Gameplay.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Rivals
{
    public class RivalObject : Rival
    {
        #region Editor

        #endregion

        #region Fields

        private float _damage;
        private float _totalGameSequences;
        private float _sequencesLostCounter = 0;

        #endregion

        #region Methods

        public override Queue<int> GetNewGameSequence()
        {
            var max = GameCore.Instance.MaxRuneIndex;
            var min = GameCore.Instance.MinRuneIndex;
            var total = Random.Range(_params.MinGameSequenceLength, _params.MaxGameSequenceLength);
            Queue<int> gameSequence = new Queue<int>();
            for (var i = 0; i < total; i++)
            {
                gameSequence.Enqueue(Random.Range(min, max));
            }

            return gameSequence;
        }

        public override void Initialize()
        {
            _damage = _params.Damage;
            _totalGameSequences = _params.GameSequences;
            _image.texture = _params.Image;
        }

        public override void PlayIntroAudio()
        {
            throw new System.NotImplementedException();
        }

        public override void PlayAttackAudio()
        {
            throw new System.NotImplementedException();
        }

        public override void PlayDefeatAudio()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
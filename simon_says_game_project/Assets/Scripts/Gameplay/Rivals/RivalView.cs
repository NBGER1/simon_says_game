using System.Collections.Generic;
using Gameplay.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Rivals
{
    public class RivalView : MonoBehaviour
    {
        #region Editor

        [SerializeField] private RawImage _image;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private TextMeshProUGUI _name;

        #endregion

        #region Fields

        private RivalModel _rivalModel;

        #endregion

        #region Methods

        public void Initialize(RivalModel rivalModel)
        {
            _rivalModel = rivalModel;
            _image.texture = _rivalModel.Image;
            _name.text = _rivalModel.Name;
        }


        public Queue<int> GetNewGameSequence()
        {
            var max = GameCore.Instance.MaxRuneIndex;
            var min = GameCore.Instance.MinRuneIndex;
            var total = Random.Range(_rivalModel.MinGameSequenceLength, _rivalModel.MaxGameSequenceLength);
            Queue<int> gameSequence = new Queue<int>();
            for (var i = 0; i < total; i++)
            {
                gameSequence.Enqueue(Random.Range(min, max));
            }

            return gameSequence;
        }

        public void PlayIntroAudio()
        {
            _audioSource.PlayOneShot(_rivalModel.IntroAudio);
        }

        public void PlayAttackAudio()
        {
            _audioSource.PlayOneShot(_rivalModel.AttackAudio);
        }

        public void PlayDefeatAudio()
        {
            _audioSource.PlayOneShot(_rivalModel.DefeatAudio);
        }

        #endregion
    }
}
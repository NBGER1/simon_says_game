using System;
using Gameplay.Core;
using Gameplay.Rune;
using Infrastructure.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.RuneObject
{
    public class RuneView : MonoBehaviour
    {
        #region Editor

        [SerializeField] private RawImage _image;
        [SerializeField] private GameObject _highlight;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private RuneParams _runeParams;

        #endregion

        #region Fields

        private int _gameIndex;

        #endregion

        #region Methods

        public void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            _image.texture = _runeParams.Image;
            _gameIndex = _runeParams.GameIndex;
            _audioSource.clip = _runeParams.Audio;
            DeselectRune();
        }


        public void SelectRune()
        {
            GameplayServices.CoroutineService
                .WaitFor(1)
                .OnStart(() =>
                {
                    HighlightRune();
                    GameCore.Instance.ComparePlayerSelection(_runeParams.GameIndex);
                })
                .OnEnd(DeselectRune);
        }

        public void HighlightRune()
        {
            _highlight.SetActive(true);
            PlayAudio();
        }

        public void DeselectRune()
        {
            _highlight.SetActive(false);
        }

        public void PlayAudio()
        {
            _audioSource.Play();
        }

        #endregion

        #region Properties

        public int GameIndex => _gameIndex;

        #endregion
    }
}
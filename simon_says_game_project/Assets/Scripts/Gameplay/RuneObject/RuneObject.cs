using System;
using System.Security.Cryptography;
using Gameplay.Core;
using Gameplay.Rune;
using Infrastructure.Services;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Gameplay.RuneObject.Factories
{
    public class RuneObject : Rune.Base.Rune
    {
        #region Editor

        [SerializeField] private RuneParams _params;
        [SerializeField] private GameObject _highlight;
        [SerializeField] private AudioSource _audioSource;

        #endregion

        #region Fields

        private int _gameIndex;

        #endregion

        #region Methods

        private void Start()
        {
            Initialize();
        }

        public override void Initialize()
        {
            _image.texture = _params.Image;
            _gameIndex = _params.GameIndex;
            _audioSource.clip = _params.Audio;
            DeselectRune();
        }


        public override void SelectRune()
        {
            GameplayServices.CoroutineService
                .WaitFor(1)
                .OnStart(() =>
                {
                    HighlightRune();
                    GameCore.Instance.ComparePlayerSelection(_params.GameIndex);
                })
                .OnEnd(DeselectRune);
        }

        public override void HighlightRune()
        {
            _highlight.SetActive(true);
            PlayAudio();
        }

        public override void DeselectRune()
        {
            _highlight.SetActive(false);
        }

        public override void PlayAudio()
        {
            _audioSource.Play();
        }

        #endregion

        #region Properties

        public int GameIndex => _gameIndex;

        #endregion
    }
}
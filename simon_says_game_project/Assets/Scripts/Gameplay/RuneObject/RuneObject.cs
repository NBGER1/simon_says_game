using System;
using Gameplay.Core;
using Gameplay.Rune;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Gameplay.RuneObject.Factories
{
    public class RuneObject : Rune.Base.Rune
    {
        #region Editor

        [SerializeField] private RuneParams _params;
        [SerializeField] private GameObject _highlight;

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
            DeselectRune();
        }


        public override void SelectRune()
        {
            HighlightRune();
            GameCore.Instance.ComparePlayerSelection(_params.GameIndex);
        }

        public override void HighlightRune()
        {
            Debug.Log($"Highlighting {_gameIndex}");
            _highlight.SetActive(true);
        }

        public override void DeselectRune()
        {
            _highlight.SetActive(false);
        }

        public override void PlayAudio()
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Properties

        public int GameIndex => _gameIndex;

        #endregion
    }
}
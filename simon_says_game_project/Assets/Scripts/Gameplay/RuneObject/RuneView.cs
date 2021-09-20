using System;
using Gameplay.Core;
using Gameplay.Rune;
using Infrastructure.Events;
using Infrastructure.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.RuneObject
{
    public class RuneView : MonoBehaviour
    {
        #region Editor

        [SerializeField] private Button _button;
        [SerializeField] private RawImage _image;
        [SerializeField] private GameObject _highlight;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private RuneParams _runeParams;

        #endregion

        #region Fields

        private int _gameIndex;
        private float _selectionDelay;

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
            GameplayServices.EventBus.Subscribe(EventTypes.OnRivalTurn, OnRivalTurn);
            GameplayServices.EventBus.Subscribe(EventTypes.OnPlayerTurn, OnPlayerTurn);
            GameplayServices.EventBus.Subscribe(EventTypes.OnRuneSelection, OnRuneSelection);
            GameplayServices.EventBus.Subscribe(EventTypes.OnRuneSelectionEnd, OnRuneSelectionEnd);

            _selectionDelay = GameCore.Instance.GameModel.RuneSelectionDelay;

            DeselectRune();
        }

        private void OnRuneSelectionEnd(EventParams obj)
        {
            _button.interactable = true;
        }

        private void OnRuneSelection(EventParams obj)
        {
            _button.interactable = false;
        }

        private void OnRivalTurn(EventParams eventParams)
        {
            _button.interactable = false;
        }

        private void OnPlayerTurn(EventParams eventParams)
        {
            _button.interactable = true;
        }

        public void SelectRune()
        {
            GameplayServices.CoroutineService
                .WaitFor(_selectionDelay)
                .OnStart(() =>
                {
                    var eventParams = EventParams.Empty;
                    GameplayServices.EventBus.Publish(EventTypes.OnRuneSelection, eventParams);
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
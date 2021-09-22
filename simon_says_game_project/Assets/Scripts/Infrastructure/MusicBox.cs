using System;
using Infrastructure.Abstracts;
using UnityEngine;

namespace Infrastructure
{
    public class MusicBox : Singleton<MusicBox>
    {
        #region Editor

        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _music;

        #endregion

        #region Field

        private bool _initialized;

        #endregion

        private void Awake()
        {
            var musicObj = GameObject.FindWithTag("Music");
            if (musicObj.Equals(this)) Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
            StartMusic();
        }

        protected override MusicBox GetInstance()
        {
            return this;
        }

        private void StartMusic()
        {
            if (_audioSource.isPlaying) return;
            _audioSource.clip = _music;
            _audioSource.loop = true;
            _audioSource.Play();
        }
    }
}
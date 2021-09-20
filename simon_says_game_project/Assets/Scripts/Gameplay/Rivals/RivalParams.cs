using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Rivals
{
    [CreateAssetMenu(menuName = "Gameplay Params/Rival", fileName = "RivalParams")]
    public class RivalParams : ScriptableObject
    {
        #region Editor

        [SerializeField] private Texture2D _image;
        [SerializeField] private string _name;
        [SerializeField] [Range(0, 5)] private int _totalGameSequences;
        [SerializeField] private int _minGameSequenceLength;
        [SerializeField] [Range(2, 4)] private int _maxGameSequenceLength;
        [SerializeField] [Range(0f, 100f)] private float _damage;
        [SerializeField] private AudioClip _introAudio;
        [SerializeField] private AudioClip _attackAudio;
        [SerializeField] private AudioClip _defeatAudio;

        #endregion

        #region Properties

        public Texture2D Image => _image;
        public string Name => _name;
        public int GameSequences => _totalGameSequences;
        public float Damage => _damage;
        public AudioClip IntroAudio => _introAudio;
        public AudioClip AttackAudio => _attackAudio;
        public AudioClip DefeatAudio => _defeatAudio;
        public int MinGameSequenceLength => _minGameSequenceLength;
        public int MaxGameSequenceLength => _maxGameSequenceLength;

        #endregion
    }
}
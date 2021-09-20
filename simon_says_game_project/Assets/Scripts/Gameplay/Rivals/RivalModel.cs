using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Rivals
{
    [CreateAssetMenu(menuName = "Gameplay Models/Rival", fileName = "RivalModel")]
    public class RivalModel : ScriptableObject
    {
        #region Editor

        [SerializeField] private Texture2D _image;
        [SerializeField] private string _name;
        [SerializeField] [Range(1f, 1000f)] private float _health;
        [SerializeField] [Range(1f, 1000f)] private float _selfDamage;
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
        public float Health => _health;
        public float SelfDamage => _selfDamage;
        public float Damage => _damage;
        public int MinGameSequenceLength => _minGameSequenceLength;
        public int MaxGameSequenceLength => _maxGameSequenceLength;
        public AudioClip IntroAudio => _introAudio;
        public AudioClip AttackAudio => _attackAudio;
        public AudioClip DefeatAudio => _defeatAudio;

        #endregion
    }
}
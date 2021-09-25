using UnityEngine;

namespace Infrastructure.Services
{
    [CreateAssetMenu(menuName = "Gameplay Services/SceneSFX", fileName = "SceneSFXModel")]
    public class SceneSFXModel : ScriptableObject
    {
        #region Editor

        [SerializeField] private AudioClip _roundWinClip;
        [SerializeField] private AudioClip _roundLoseClip;
        [SerializeField] private AudioClip _gameOverWinClip;
        [SerializeField] private AudioClip _onTakenDamageClip;
        [SerializeField] private AudioClip _onUIButtonClick;

        #endregion

        #region Properties

        public AudioClip RoundWinClip => _roundWinClip;
        public AudioClip RoundLoseClip => _roundLoseClip;
        public AudioClip GameOverWinClip => _gameOverWinClip;
        public AudioClip OnTakenDamageClip => _onTakenDamageClip;
        public AudioClip OnUIButtonclick => _onUIButtonClick;
        #endregion
    }
}
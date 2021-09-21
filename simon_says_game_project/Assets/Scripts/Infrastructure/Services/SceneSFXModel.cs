using UnityEngine;

namespace Infrastructure.Services
{
    [CreateAssetMenu(menuName = "Gameplay Services/SceneSFX", fileName = "SceneSFXModel")]
    public class SceneSFXModel : ScriptableObject
    {
        #region Editor

        [SerializeField] private AudioClip _roundWinClip;
        [SerializeField] private AudioClip _roundLoseClip;

        #endregion

        #region Properties

        public AudioClip RoundWinClip => _roundWinClip;
        public AudioClip RoundLoseClip => _roundLoseClip;

        #endregion
    }
}
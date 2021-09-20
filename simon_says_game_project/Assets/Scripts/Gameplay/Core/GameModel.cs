using UnityEngine;

namespace Gameplay.Core
{
    [CreateAssetMenu(menuName = "Gameplay Params/GameCore", fileName = "GameParams")]
    public class GameModel : ScriptableObject
    {
        #region Editor

        [SerializeField] [Range(0, 1000)] private int _runesInScene;

        #endregion

        #region Properties

        public int RunesInScene => _runesInScene;

        #endregion
    }
}
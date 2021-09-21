using UnityEngine;

namespace Gameplay.Core
{
    [CreateAssetMenu(menuName = "Gameplay Params/GameCore", fileName = "GameParams")]
    public class GameModel : ScriptableObject
    {
        #region Editor

        [SerializeField] [Range(0, 1000)] private int _runesInScene;
        [SerializeField] [Range(0, 5f)] private float _runeSelectionDelay;
        [SerializeField] [Range(0, 60)] private int _gameTimer;
        #endregion

        #region Properties

        public int RunesInScene => _runesInScene;
        public float RuneSelectionDelay => _runeSelectionDelay;
        public int GameTimer => _gameTimer;

        #endregion
    }
}
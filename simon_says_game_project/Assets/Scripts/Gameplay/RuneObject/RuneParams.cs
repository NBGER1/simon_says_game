using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Rune
{
    [CreateAssetMenu(menuName = "Gameplay Params/Rune", fileName = "RuneParams")]
    public class RuneParams : ScriptableObject
    {
        #region Editor

        [SerializeField] private Texture2D _image;
        [SerializeField] private string _name;
        [SerializeField] private int _gameIndex;

        #endregion

        #region Properties

        public Texture2D Image => _image;
        public string Name => _name;
        public int GameIndex => _gameIndex;

        #endregion
    }
}
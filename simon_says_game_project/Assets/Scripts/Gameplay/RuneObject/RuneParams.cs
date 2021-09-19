using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Rune
{
    [CreateAssetMenu(menuName = "Gameplay Params/Rune", fileName = "RuneParams")]
    public class RuneParams : ScriptableObject
    {
        #region Editor

        [SerializeField] private RawImage _image;
        [SerializeField] private string _name;

        #endregion

        #region Properties

        public RawImage Image => _image;
        public string Name => _name;

        #endregion
    }
}
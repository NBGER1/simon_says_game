using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Rivals
{
    [CreateAssetMenu(menuName = "Gameplay Params/Rival", fileName = "RivalParams")]
    public class RivalParams : ScriptableObject
    {
        #region Editor

        [SerializeField] private RawImage _image;
        [SerializeField] private string _name;
        [SerializeField] private int[] _sequences;
        [SerializeField] private float _damage;

        #endregion

        #region Properties

        public RawImage Image => _image;
        public string Name => _name;
        public int[] Sequences => _sequences;
        public float Damage => _damage;

        #endregion
    }
}
using Gameplay.RuneObject;
using UnityEngine;

namespace Infrastructure.Services
{
    [CreateAssetMenu(menuName = "Services/Elements/Rune Elements", fileName = "RunesElements")]
    public class RunesElements : ScriptableObject
    {
        #region Editor

        [SerializeField] private RuneView[] _runes;

        #endregion

        #region Methods

        public RuneView GetRuneByIndex(int index)
        {
            if (index < _runes.Length)
            {
                return _runes[index];
            }

            return null;
        }

        #endregion
    }
}
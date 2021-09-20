using Gameplay.RuneObject;
using UnityEngine;

namespace Infrastructure.Services
{
    [CreateAssetMenu(menuName = "Services/Elements/Rune Elements", fileName = "RunesElements")]
    public class RunesElements : ScriptableObject
    {
        #region Editor

        [SerializeField] private RuneView _runeEhwaz;
        [SerializeField] private RuneView _runeFehu;

        #endregion

        #region Properties

        public RuneView Ehwaz => _runeEhwaz;
        public RuneView Fehu => _runeFehu;

        #endregion
    }
}
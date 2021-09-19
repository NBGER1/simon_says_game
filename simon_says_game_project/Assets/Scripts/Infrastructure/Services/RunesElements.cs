using Gameplay.Rune.Base;
using UnityEngine;

namespace Infrastructure.Services
{
    [CreateAssetMenu(menuName = "Services/Elements/Rune Elements", fileName = "RunesElements")]
    public class RunesElements : ScriptableObject
    {
        #region Editor

        [SerializeField] private Rune _runeEhwaz;
        [SerializeField] private Rune _runeFehu;

        #endregion

        #region Properties

        public Rune Ehwaz => _runeEhwaz;
        public Rune Fehu => _runeFehu;

        #endregion
    }
}
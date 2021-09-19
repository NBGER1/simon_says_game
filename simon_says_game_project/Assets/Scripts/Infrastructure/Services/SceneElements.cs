using UnityEngine;

namespace Infrastructure.Services
{
    [CreateAssetMenu(menuName = "Services/Elements/Scene Elements", fileName = "SceneElements")]
    public class SceneElements : ScriptableObject
    {
        #region Editor

        [SerializeField] private GameObject _runeArea;

        #endregion

        #region Properties

        public GameObject RuneArea => _runeArea;
        

        #endregion
    }
}
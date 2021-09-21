using UnityEngine;


namespace Infrastructure.Services
{
    [CreateAssetMenu(menuName = "Gameplay Services/PopupElements", fileName = "PopupElements")]
    public class PopupElements : ScriptableObject
    {
        #region Editor

        [SerializeField] private GameObject _losePopup;
        [SerializeField] private GameObject _winPopup;
        [SerializeField] private GameObject _scoreboardEntryPrefab;

        #endregion

        #region Properties

        public GameObject WinPopup => _winPopup;
        public GameObject LosePopup => _losePopup;
        public GameObject ScoreboardEntryPrefab => _scoreboardEntryPrefab;

        #endregion
    }
}
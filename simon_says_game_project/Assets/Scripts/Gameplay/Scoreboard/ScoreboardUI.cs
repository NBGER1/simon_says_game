using Gameplay.Core;
using Infrastructure.Managers;
using UnityEngine;

namespace Gameplay.Scoreboard
{
    public class ScoreboardUI : MonoBehaviour
    {
        #region Editor

        [SerializeField] private GameObject _container;
        [SerializeField] private GameObject _entryPrefab;

        #endregion

        #region Methods

        public void AddNewEntry(ScoreboardEntryParams scoreboardEntryParams)
        {
            var prefab = Instantiate(_entryPrefab, _container.transform, true);
            //   var entryObj = Instantiate(_entryPrefab, _container.transform);
            prefab.GetComponent<ScoreboardEntry>().Initialize(scoreboardEntryParams);
        }

        #endregion
    }
}
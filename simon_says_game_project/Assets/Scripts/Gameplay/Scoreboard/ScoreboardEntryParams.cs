using System;
using UnityEngine;

namespace Gameplay.Scoreboard
{
    [Serializable]
    public class ScoreboardEntryParams
    {
        #region Fields

        [SerializeField] private int _score;
        [SerializeField] private string _name;
        [SerializeField] private string _textureName;

        #endregion

        #region Constructor

        public ScoreboardEntryParams(int score, string name, string textureName)
        {
            _score = score;
            _name = name;
            _textureName = textureName;
        }

        #endregion

        #region Properties

        public int Score => _score;
        public string Name => _name;
        public string TextureName => _textureName;

        #endregion
    }
}
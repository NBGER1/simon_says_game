using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Scoreboard
{
    public class ScoreboardEntry : MonoBehaviour
    {
        #region Editor

        [SerializeField] private RawImage _image;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _score;

        #endregion

        #region Methods

        public void Initialize(ScoreboardEntryParams scoreboardEntryParams)
        {
            var texture = (Texture2D) Resources.Load(scoreboardEntryParams.TextureName + ".png");
            _image.texture = texture;
            _name.text = scoreboardEntryParams.Name;
            _score.text = scoreboardEntryParams.Score.ToString();
        }

        #endregion
    }
}
using Gameplay.Core;
using UnityEngine;

public class LosePopup : MonoBehaviour
{
    #region Methods

    public void Continue()
    {
        //TODO Register new entry to leaderboard
        //TODO Show leaderboard popup
        GameCore.Instance.ResetStage();
        Destroy(gameObject);
    }

    #endregion
}
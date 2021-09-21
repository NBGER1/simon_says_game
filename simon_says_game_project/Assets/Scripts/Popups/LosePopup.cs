using Gameplay.Core;
using Infrastructure.Managers;
using UnityEngine;

public class LosePopup : MonoBehaviour
{
    #region Methods

    public void Continue()
    {
        SceneManager.MoveToMainMenuScene();
        Destroy(gameObject);
    }

    #endregion
}
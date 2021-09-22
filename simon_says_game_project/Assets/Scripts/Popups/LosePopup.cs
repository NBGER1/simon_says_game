using UnityEngine;
using SceneManager = Infrastructure.Managers.SceneManager;

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
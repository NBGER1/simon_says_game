using Gameplay.Core;
using Infrastructure.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using SceneManager = Infrastructure.Managers.SceneManager;

public class LosePopup : MonoBehaviour
{
    #region Methods

    public void Continue()
    {
        GameCore.Instance.ResetStage();
        Destroy(gameObject);
    }

    #endregion
}
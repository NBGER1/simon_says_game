﻿using Gameplay.Core;
using Infrastructure.Managers;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
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
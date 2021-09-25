using Infrastructure.Database;
using Infrastructure.Events;
using Infrastructure.Services;
using TMPro;
using UnityEngine;
using SceneManager = Infrastructure.Managers.SceneManager;

public class LosePopup : MonoBehaviour
{
    #region Editor

    [SerializeField] private TextMeshProUGUI _scoreValue;

    #endregion
    #region Methods

    private void OnEnable()
    {
        _scoreValue.text = PlayerData.Instance.Score.ToString();
    }
    public void Continue()
    {
        var eParams = EventParams.Empty;
        GameplayServices.EventBus.Publish(EventTypes.OnUIButtonClick, eParams);
        SceneManager.MoveToMainMenuScene();
        Destroy(gameObject);
    }

    #endregion
}
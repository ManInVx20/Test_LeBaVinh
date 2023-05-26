using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseCanvas : UICanvas
{
    [SerializeField]
    private Button _continueButton;
    [SerializeField]
    private Button _settingsButton;
    [SerializeField]
    private Button _exitButton;

    private void Awake()
    {
        _continueButton.onClick.AddListener(() =>
        {
            GameManager.Instance.UnpauseGame();
        });
        _settingsButton.onClick.AddListener(() =>
        {

        });
        _exitButton.onClick.AddListener(() =>
        {
            GameManager.Instance.ExitGame();
        });
    }
}

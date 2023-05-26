using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuCanvas : UICanvas
{
    [SerializeField]
    private Button _playButton;
    [SerializeField]
    private Button _settingsButton;

    private void Awake()
    {
        _playButton.onClick.AddListener(() =>
        {
            GameManager.Instance.StartGame();
        });
        _settingsButton.onClick.AddListener(() =>
        {

        });
    }
}

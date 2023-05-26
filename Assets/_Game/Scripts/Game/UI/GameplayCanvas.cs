using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayCanvas : UICanvas
{
    [SerializeField]
    private Button _pauseButton;

    private void Awake()
    {
        _pauseButton.onClick.AddListener(() =>
        {
            GameManager.Instance.PauseGame();
        });
    }
}

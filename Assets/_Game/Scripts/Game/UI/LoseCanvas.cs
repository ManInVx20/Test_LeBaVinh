using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseCanvas : UICanvas
{
    [SerializeField]
    private Button _exitButton;

    private void Awake()
    {
        _exitButton.onClick.AddListener(() =>
        {
            GameManager.Instance.ExitGame();
        });
    }
}

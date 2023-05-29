using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayCanvas : UICanvas
{
    [SerializeField]
    private Button _pauseButton;
    [SerializeField]
    private TextMeshProUGUI _levelText;

    private void Awake()
    {
        _pauseButton.onClick.AddListener(() =>
        {
            GameManager.Instance.PauseGame();
        });
    }

    public override void Setup()
    {
        base.Setup();

        _levelText.text = $"Level {LevelManager.Instance.GetCurrentLevel()}";
    }
}

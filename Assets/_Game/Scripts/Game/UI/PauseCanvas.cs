using System;
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
    [SerializeField]
    private Transform _buffsTransform;
    [SerializeField]
    private BuffUI _buffUIPrefab;

    private List<BuffUI> _buffUIList = new List<BuffUI>();

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

    public override void Setup()
    {
        base.Setup();

        CreateBuffUIs();
    }

    private void CreateBuffUIs()
    {
        for (int i = _buffUIList.Count - 1; i >= 0; i--)
        {
            _buffUIList[i].Despawn();

            _buffUIList.RemoveAt(i);
        }

        List<Buff> buffList = Player.Instance.GetBuffList();
        for (int i = 0; i < buffList.Count; i++)
        {
            BuffUI buffUI = Instantiate(_buffUIPrefab, _buffsTransform);
            buffUI.Initialize(buffList[i].Sprite);

            _buffUIList.Add(buffUI);
        }
    }
}

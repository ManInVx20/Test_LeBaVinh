using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private enum State
    {
        GameWaited = 0,
        GameStarted = 1,
        GamePaused = 2,
    }

    [SerializeField]
    private CameraFollow _cameraFollow;

    private State _state;
    private Player _player;

    private void Start()
    {
        WaitGame();
    }

    public void WaitGame()
    {
        _state = State.GameWaited;

        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<MainMenuCanvas>();
    }

    public void StartGame()
    {
        _state = State.GameStarted;

        LevelManager.Instance.LoadLevel();

        _player = Instantiate(ResourceManager.Instance.PlayerPrefab);
        _cameraFollow.SetTarget(_player.GetTransform());

        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<GameplayCanvas>();
        UIManager.Instance.OpenUI<ControlCanvas>();
    }

    public void ExitGame()
    {
        Time.timeScale = 1.0f;

        LevelManager.Instance.UnloadLevel();

        _player?.Despawn();
        _cameraFollow.SetTarget(null);

        WaitGame();
    }

    public void PauseGame()
    {
        Time.timeScale = 0.0f;

        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<PauseCanvas>();
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1.0f;

        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<GameplayCanvas>();
        UIManager.Instance.OpenUI<ControlCanvas>();
    }
}

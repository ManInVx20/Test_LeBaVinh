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
        GameFinished = 3,
    }

    private State _state;
    private Player _player;
    private CameraFollow[] _cameraFollowArray;

    private void Awake()
    {
        _cameraFollowArray = FindObjectsOfType<CameraFollow>();
    }

    private void Start()
    {
        WaitGame();
    }

    public bool IsGameWaited()
    {
        return _state == State.GameWaited;
    }

    public bool IsGameStarted()
    {
        return _state == State.GameStarted;
    }

    public bool IsGameFinished()
    {
        return _state == State.GameFinished;
    }

    public void WaitGame()
    {
        _state = State.GameWaited;

        LevelManager.Instance.ResetLevelIndex();

        for (int i = 0; i < _cameraFollowArray.Length; i++)
        {
            _cameraFollowArray[i].SetTarget(null);
        }

        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<MainMenuCanvas>();
    }

    public void StartGame()
    {
        _state = State.GameStarted;

        LevelManager.Instance.LoadLevel(() =>
        {
            UIManager.Instance.CloseAll();
            UIManager.Instance.OpenUI<LoadingCanvas>();
        }, () =>
        {
            if (_player == null)
            {
                _player = Instantiate(ResourceManager.Instance.PlayerPrefab);
            }

            if (_player != null)
            {
                _player.GetTransform().position = FindObjectOfType<Level>().StartPosition;

                for (int i = 0; i < _cameraFollowArray.Length; i++)
                {
                    _cameraFollowArray[i].SetTarget(Player.Instance.GetTransform());
                }
            }


            UIManager.Instance.CloseAll();
            UIManager.Instance.OpenUI<GameplayCanvas>();
            UIManager.Instance.OpenUI<ControlCanvas>();
        });
    }

    public void FinishGame(bool isWon)
    {
        _state = State.GameFinished;

        if (isWon)
        {
            // Win
            UIManager.Instance.CloseAll();
            UIManager.Instance.OpenUI<WinCanvas>();

            Player.Instance.SetMoveDirection(Vector3.zero);
        }
        else
        {
            // Lose
            UIManager.Instance.CloseAll();
            UIManager.Instance.OpenUI<LoseCanvas>();
        }
    }

    public void ExitGame()
    {
        Time.timeScale = 1.0f;

        LevelManager.Instance.UnloadLevel();

        if (_player != null)
        {
            _player.Despawn();
        }

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

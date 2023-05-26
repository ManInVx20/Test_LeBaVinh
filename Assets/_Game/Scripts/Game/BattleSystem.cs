using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : CustomMonoBehaviour
{
    public event EventHandler OnBattleStart;
    public event EventHandler OnBattleOver;

    [Serializable]
    private class Wave
    {
        [SerializeField]
        private Enemy[] _enemyArray;

        private bool _isSpawned;

        public void SpawnEnemies()
        {
            _isSpawned = true;

            for (int i = 0; i < _enemyArray.Length; i++)
            {
                _enemyArray[i].Spawn();
            }
        }

        public bool IsSpawned()
        {
            return _isSpawned;
        }

        public bool IsOver()
        {
            for (int i = 0; i < _enemyArray.Length; i++)
            {
                if (!_enemyArray[i].IsDead())
                {
                    return false;
                }
            }

            return true;
        }
    }

    private enum State
    {
        Idle = 0,
        Active = 1,
        Over = 2,
    }

    [SerializeField]
    private ColliderTrigger _colliderTrigger;
    [SerializeField]
    private Wave[] _waveArray;

    private State _state = State.Idle;

    private void Start()
    {
        _colliderTrigger.OnPlayerEnterTrigger += ColliderTrigger_OnPlayerEnterTrigger;
    }

    private void Update()
    {
        switch (_state)
        {
            case State.Idle:
                break;
            case State.Active:
                for (int i = 0; i < _waveArray.Length; i++)
                {
                    if (((i - 1) < 0 || _waveArray[i - 1].IsOver()) && !_waveArray[i].IsSpawned())
                    {
                        _waveArray[i].SpawnEnemies();
                    }
                }

                CheckBattleOver();

                break;
            case State.Over:
                break;
        }
    }

    private void ColliderTrigger_OnPlayerEnterTrigger(object sender, EventArgs e)
    {
        if (_state == State.Idle)
        {
            StartBattle();

            _colliderTrigger.OnPlayerEnterTrigger -= ColliderTrigger_OnPlayerEnterTrigger;
        }
    }

    private void StartBattle()
    {
        Debug.Log("Active");
        _state = State.Active;

        OnBattleStart?.Invoke(this, EventArgs.Empty);
    }

    private void CheckBattleOver()
    {
        if (_state == State.Active)
        {
            if (AreWavesOver())
            {
                Debug.Log("Over");
                _state = State.Over;

                OnBattleOver?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    private bool AreWavesOver()
    {
        for (int i = 0; i < _waveArray.Length; i++)
        {
            if (!_waveArray[i].IsOver())
            {
                return false;
            }
        }

        return true;
    }
}

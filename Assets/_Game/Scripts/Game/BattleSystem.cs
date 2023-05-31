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
        private EnemySpawn[] _enemySpawnArray;

        private bool _isSpawned;

        public void SpawnEnemies()
        {
            _isSpawned = true;

            for (int i = 0; i < _enemySpawnArray.Length; i++)
            {
                _enemySpawnArray[i].Spawn();
            }
        }

        public bool IsSpawned()
        {
            return _isSpawned;
        }

        public bool IsOver()
        {
            for (int i = 0; i < _enemySpawnArray.Length; i++)
            {
                if (!_enemySpawnArray[i].Enemy.IsDead())
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

    //TODO:
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
        _state = State.Active;

        OnBattleStart?.Invoke(this, EventArgs.Empty);
    }

    private void CheckBattleOver()
    {
        if (_state == State.Active)
        {
            if (AreWavesOver())
            {
                _state = State.Over;

                OnBattleOver?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    private bool AreWavesOver()
    {
        for (int i = 0; i < _waveArray.Length; i++)
        {
            if (!_waveArray[i].IsSpawned() || !_waveArray[i].IsOver())
            {
                return false;
            }
        }

        return true;
    }

    //List<Enemy> enemies = new List<Enemy>();

    //private void InitWave()
    //{
    //    //doc data
    //    //for
    //    EnemyInit();
     
    //}

    //void FinishWave()
    //{

    //}

    //public void EnemyInit()
    //{
    //    //spawn enemy 
    //    //add enemy vao list
    //    //Enemy e = Instantiate();
    //    //e.onDeathAction = EnemyDeath;
    //}

    //public void EnemyDeath(Enemy enemy)
    //{
    //    //remove enemy list
    //    //check next wave
    //}
}

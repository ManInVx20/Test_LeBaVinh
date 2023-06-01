using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : CustomMonoBehaviour
{
    [Serializable]
    public struct Wave
    {
        public List<EnemySpawn> EnemySpawnList;
    }

    public event EventHandler OnBattleStarted;
    public event EventHandler OnBattleFinished;

    [SerializeField]
    private ColliderTrigger _colliderTrigger;
    [SerializeField]
    private List<Wave> _waveList;

    private int _waveIndex = -1;
    private List<Enemy> _enemyList = new List<Enemy>();

    private void Start()
    {
        _colliderTrigger.OnPlayerEnterTriggerCollider += ColliderTrigger_OnPlayerEnterTriggerCollider;
    }

    private void ColliderTrigger_OnPlayerEnterTriggerCollider(object sender, EventArgs args)
    {
        StartBattle();

        _colliderTrigger.OnPlayerEnterTriggerCollider -= ColliderTrigger_OnPlayerEnterTriggerCollider;
    }

    private void StartBattle()
    {
        if (_waveList.Count == 0)
        {
            return;
        }

        _waveIndex = 0;

        SpawnEnemies(_waveList[_waveIndex]);

        OnBattleStarted?.Invoke(this, EventArgs.Empty);
    }

    private void FinishBattle()
    {
        OnBattleFinished?.Invoke(this, EventArgs.Empty);
    }

    private void SpawnEnemies(Wave wave)
    {
        for (int i = 0; i < wave.EnemySpawnList.Count; i++)
        {
            Enemy enemy = wave.EnemySpawnList[i].Spawn();
            enemy.OnCharacterDespawned += Enemy_OnCharacterDespawned;

            _enemyList.Add(enemy);
        }
    }

    private void Enemy_OnCharacterDespawned(object sender, EventArgs args)
    {
        Enemy enemy = sender as Enemy;

        _enemyList.Remove(enemy);
        if (_enemyList.Count == 0)
        {
            if (_waveIndex + 1 < _waveList.Count)
            {
                _waveIndex += 1;

                SpawnEnemies(_waveList[_waveIndex]);
            }
            else
            {
                FinishBattle();
            }
        }

        enemy.OnCharacterDespawned -= Enemy_OnCharacterDespawned;
    }
}

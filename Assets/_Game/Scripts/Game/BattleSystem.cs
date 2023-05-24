using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : CustomMonoBehaviour
{
    private enum State
    {
        Idle = 0,
        Active = 1,
    }

    [SerializeField]
    private ColliderTrigger _colliderTrigger;
    [SerializeField]
    private EnemySpawner _enemySpawner;

    private State _state = State.Idle;

    private void Start()
    {
        _colliderTrigger.OnPlayerEnterTrigger += ColliderTrigger_OnPlayerEnterTrigger;
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
        Debug.Log("Start");
        _state = State.Active;

        _enemySpawner.Spawn();
    }
}

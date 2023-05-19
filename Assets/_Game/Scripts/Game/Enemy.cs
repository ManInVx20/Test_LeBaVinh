using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public EnemyIdleState IdleState { get; } = new EnemyIdleState();
    public EnemyPatrolState PatrolState { get; } = new EnemyPatrolState();

    private IEnemyState _state;
    private float _attackTimer;
    private float _attackTime;

    public override void Begin()
    {
        base.Begin();

        ChangeState(IdleState);

        _attackTimer = 0.0f;
        _attackTime = Random.Range(2.0f, 5.0f);
    }

    public override void Execute()
    {
        base.Execute();

        _state?.Execute(this);

        HandleAttack();
    }

    public void ChangeState(IEnemyState newState)
    {
        _state?.Exit(this);

        _state = newState;

        _state?.Enter(this);
    }

    private void HandleAttack()
    {
        _attackTimer += Time.deltaTime;
        if (_attackTimer >= _attackTime)
        {
            _attackTimer = 0.0f;
            _attackTime = Random.Range(2.0f, 5.0f);

            if (IsManualAttacking())
            {
                DisableManualAttack();
            }
            else
            {
                EnableManualAttack();
            }
        }
    }
}

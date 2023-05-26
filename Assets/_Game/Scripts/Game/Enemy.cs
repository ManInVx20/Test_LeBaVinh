using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public EnemyIdleState IdleState { get; } = new EnemyIdleState();
    public EnemyPatrolState PatrolState { get; } = new EnemyPatrolState();

    [Header("Prize")]
    [SerializeField]
    private float _energyPrize = 10.0f;
    [SerializeField]
    private int _goldPrize = 1;

    private IEnemyState _state;
    private float _attackTimer;
    private float _attackTime;

    public override void Begin()
    {
        base.Begin();

        ChangeState(IdleState);

        _attackTimer = 0.0f;
        _attackTime = Random.Range(2.0f, 5.0f);

        Hide();
    }

    public override void Execute()
    {
        base.Execute();

        if (_state != null)
        {
            _state.Execute(this);

            HandleAttack();
        }
    }

    public override void Die()
    {
        base.Die();

        ChangeState(null);

        Player.Instance.ChangeEnergy(_energyPrize);

        ResourceManager.Instance.TryChangeGold(_goldPrize);
    }

    public void ChangeState(IEnemyState newState)
    {
        _state?.Exit(this);

        _state = newState;

        _state?.Enter(this);
    }

    public void Spawn()
    {
        Show();
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

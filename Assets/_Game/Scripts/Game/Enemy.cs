using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public EnemyIdleState IdleState { get; } = new EnemyIdleState();
    public EnemyPatrolState PatrolState { get; } = new EnemyPatrolState();

    [FloatRangeSlider(0.1f, 100.0f)]
    [SerializeField]
    private FloatRange _attackTimeRange = new FloatRange();

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
        _attackTime = _attackTimeRange.RandomValueInRange;

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
            _attackTime = _attackTimeRange.RandomValueInRange;

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

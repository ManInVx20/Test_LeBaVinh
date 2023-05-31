using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : Enemy
{
    public MobIdleState IdleState { get; } = new MobIdleState();
    public MobPatrolState PatrolState { get; } = new MobPatrolState();

    [Header("Specific")]
    [FloatRangeSlider(0.1f, 100.0f)]
    [SerializeField]
    private FloatRange _attackTimeRange = new FloatRange();

    private float _attackTimer;
    private float _attackTime;

    public override void Begin()
    {
        base.Begin();

        ChangeState(IdleState);

        _attackTimer = 0.0f;
        _attackTime = _attackTimeRange.RandomValueInRange;
    }

    public override void Execute()
    {
        base.Execute();

        if (State != null)
        {
            State.Execute(this);

            HandleAttack();
        }
    }

    public override void Die()
    {
        base.Die();

        ChangeState(null);

        Player.Instance.ChangeEnergy(EnergyPrize);

        ResourceManager.Instance.TryChangeGold(GoldPrize);
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

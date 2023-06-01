using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public BossIdleState IdleState { get; } = new BossIdleState();
    public BossPatrolState PatrolState { get; } = new BossPatrolState();
    public BossAttackOneState AttackOneState { get; } = new BossAttackOneState();

    public override void PrepareToUse()
    {
        base.PrepareToUse();

        Begin();
    }

    public override void Begin()
    {
        base.Begin();

        ChangeState(IdleState);
    }

    public override void Execute()
    {
        base.Execute();

        if (State != null)
        {
            State.Execute(this);
        }
    }

    public override void Die()
    {
        base.Die();

        ChangeState(null);

        Player.Instance.ChangeEnergy(EnergyPrize);

        ResourceManager.Instance.TryChangeGold(GoldPrize);
    }

    public override void Despawn()
    {
        base.Despawn();

        ReturnToPool();
    }
}

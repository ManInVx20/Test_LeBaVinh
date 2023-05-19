using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : IEnemyState
{
    private float _changeStateTimer;
    private float _changeStateTime;

    public void Enter(Enemy enemy)
    {
        enemy.SetMoveDirection(Utilities.GetRandomDirection2D());

        _changeStateTimer = 0.0f;
        _changeStateTime = Random.Range(1.0f, 3.0f);
    }

    public void Execute(Enemy enemy)
    {
        _changeStateTimer += Time.deltaTime;
        if (_changeStateTimer > _changeStateTime)
        {
            _changeStateTimer = 0.0f;

            if (Random.Range(0, 3) > 0)
            {
                enemy.ChangeState(enemy.IdleState);
            }
            else
            {
                enemy.ChangeState(enemy.PatrolState);
            }
        }
    }

    public void Exit(Enemy enemy)
    {

    }
}

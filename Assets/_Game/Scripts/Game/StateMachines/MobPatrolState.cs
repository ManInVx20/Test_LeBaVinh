using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobPatrolState : IEnemyState
{
    private float _changeStateTimer;
    private float _changeStateTime;
    private Vector3 _moveDirection;

    public void Enter(Enemy enemy)
    {
        _moveDirection = Utilities.GetRandomDirection2D();
        enemy.SetMoveDirection(_moveDirection);

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
                enemy.ChangeState(((Mob)enemy).IdleState);
            }
            else
            {
                enemy.ChangeState(((Mob)enemy).PatrolState);
            }
        }
        else
        {
            while (enemy.HasObstacle(_moveDirection))
            {
                _moveDirection = Utilities.GetRandomDirection2D();
            }

            enemy.SetMoveDirection(_moveDirection);
        }    
    }

    public void Exit(Enemy enemy)
    {

    }
}

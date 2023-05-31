using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackOneState : IEnemyState
{
    private int _attackCountMax;
    private int _attackCount;
    private float _attackTime;
    private float _attackTimer;
    private float _baseMoveSpeed;
    private Vector3 _targetPosition;

    public void Enter(Enemy enemy)
    {
        float healthRatio = enemy.GetHealth() / enemy.GetMaxHealth();
        _attackCountMax = healthRatio > 0.5f ? 2 : 4;
        _attackCount = 0;
        _attackTime = 1.5f;
        _attackTimer = _attackTime;

        enemy.SetMoveDirection(Vector3.zero);
        _baseMoveSpeed = enemy.GetMoveSpeed();
        enemy.ChangeMoveSpeed(_baseMoveSpeed);
        enemy.EnableManualAttack();

    }

    public void Execute(Enemy enemy)
    {
        if (_attackCount <= _attackCountMax)
        {
            _attackTimer += Time.deltaTime;
            if (_attackTimer >= _attackTime)
            {
                _attackCount += 1;
                _attackTimer = 0.0f;

                if (Player.Instance != null)
                {
                    _targetPosition = Player.Instance.GetTransform().position;
                }

                Vector3 moveDirection = (_targetPosition - enemy.GetTransform().position).normalized;
                enemy.SetMoveDirection(moveDirection);
            }
        }
        else
        {
            enemy.ChangeState(((Boss)enemy).IdleState);
        }
    }

    public void Exit(Enemy enemy)
    {
        enemy.ChangeMoveSpeed(-_baseMoveSpeed);
        enemy.DisableManualAttack();
    }
}

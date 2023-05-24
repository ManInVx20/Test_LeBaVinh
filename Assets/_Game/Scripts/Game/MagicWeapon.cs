using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicWeapon : Weapon
{
    [SerializeField]
    private Transform _shootPoint;
    [SerializeField]
    private BulletPool _bulletPool;
    [SerializeField]
    private float _delayAttackTime = 0.1f;

    private Coroutine _attackCoroutine;

    public override void Attack()
    {
        base.Attack();

        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
        }

        _attackCoroutine = StartCoroutine(AttackCoroutine(_delayAttackTime));
    }

    public override void Collect()
    {
        base.Collect();

        if (Player.Instance.IsMainWeapon())
        {
            Player.Instance.ChangeMainWeapon(this);
        }
    }

    private IEnumerator AttackCoroutine(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        int bulletCount = 4;
        Vector3[] targetShootPointEulerAnglesArray = new Vector3[bulletCount];
        float angle = Random.Range(0.0f, 45.0f), angleStep = 90.0f;
        for (int i = 0; i < bulletCount; i++)
        {
            targetShootPointEulerAnglesArray[i] = _shootPoint.eulerAngles + new Vector3(0.0f, 0.0f, angle - angleStep * i);
            Bullet bullet = _bulletPool.GetPrefabInstance();
            bullet.Initialize(_shootPoint.position, targetShootPointEulerAnglesArray[i], GetOwner(), GetAttackDamage());
        }
    }
}

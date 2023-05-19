using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    public enum Type
    {
        MachineGun = 0,
        Shotgun = 1,
        Sniper = 2,
    }

    [SerializeField]
    private Type _type;
    [SerializeField]
    private Transform _shootPoint;
    [SerializeField]
    private BulletPool _bulletPool;

    public override void Attack()
    {
        base.Attack();

        Bullet bullet = null;
        switch (_type)
        {
            case Type.MachineGun:
            case Type.Sniper:
                float randomAngle = Random.Range(-1.5f, 1.5f);
                Vector3 targetShootPointEulerAngles = _shootPoint.eulerAngles + new Vector3(0.0f, 0.0f, randomAngle);
                bullet = _bulletPool.GetPrefabInstance();
                bullet.Initialize(_shootPoint.position, targetShootPointEulerAngles, GetOwner(), GetAttackDamage());

                break;
            case Type.Shotgun:
                int bulletCount = 3;
                Vector3[] targetShootPointEulerAnglesArray = new Vector3[bulletCount];
                float angle = 15.0f, angleStep = 15.0f;
                for (int i = 0; i < bulletCount; i++)
                {
                    targetShootPointEulerAnglesArray[i] = _shootPoint.eulerAngles + new Vector3(0.0f, 0.0f, angle - angleStep * i);
                    bullet = _bulletPool.GetPrefabInstance();
                    bullet.Initialize(_shootPoint.position, targetShootPointEulerAnglesArray[i], GetOwner(), GetAttackDamage());
                }

                break;
        }
    }

    public override void Collect()
    {
        base.Collect();

        if (Player.Instance.IsMainWeapon())
        {
            Player.Instance.SwapMainWeapon(this);
        }
    }
}

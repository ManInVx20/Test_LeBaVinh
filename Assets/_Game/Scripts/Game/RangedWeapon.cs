using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    public enum Type
    {
        Pistol = 0,
        MachineGun = 1,
        Shotgun = 2,
        Sniper = 3,
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
            case Type.Pistol:
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
                float angle = 10.0f, angleStep = 10.0f;

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
            Player.Instance.ChangeMainWeapon(this);
        }
    }
}

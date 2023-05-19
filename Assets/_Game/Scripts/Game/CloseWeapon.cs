using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseWeapon : Weapon
{
    public override void Attack()
    {
        base.Attack();
    }

    public override void Collect()
    {
        base.Collect();

        //if (!Player.Instance.IsMainWeapon())
        //{
        //    Player.Instance.SwapMainWeapon(this);
        //}
    }
}

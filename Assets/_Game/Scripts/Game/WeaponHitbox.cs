using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitbox : CustomMonoBehaviour
{
    [SerializeField]
    private Weapon _weapon;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (Cache.TryGetHittableComponent2D(collider, out IHittable component))
        {
            if (component.IsHit(_weapon.GetOwner(), _weapon.GetAttackDamage()))
            {
                ResourceManager.Instance.SlashVFXPool.GetPrefabInstance().Initialize(GetTransform());
            }
        }
    }
}

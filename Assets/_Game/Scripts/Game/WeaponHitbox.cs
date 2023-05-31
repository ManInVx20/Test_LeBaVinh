using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitbox : CustomMonoBehaviour
{
    [SerializeField]
    private Weapon _weapon;
    [SerializeField]
    private ParticleSystem _hitVFXPrefab;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetType().Equals(typeof(BoxCollider2D)) && Cache.TryGetCachedComponent<Character>(collider, out Character character))
        {
            if (IsValidTarget(character))
            {
                ResourceManager.Instance.SlashVFXPool.GetPrefabInstance().Initialize(GetTransform());

                character.Hit(_weapon.GetAttackDamage());
            }
        }
    }

    private bool IsValidTarget(Character character)
    {
        return !character.IsDead() && !character.GetType().Equals(_weapon.GetOwner().GetType());
    }
}

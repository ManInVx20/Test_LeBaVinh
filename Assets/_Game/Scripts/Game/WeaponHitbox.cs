using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitbox : MonoBehaviour
{
    [SerializeField]
    private Weapon _weapon;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetType().Equals(typeof(BoxCollider2D)) && Cache.TryGetCachedComponent2D<Character>(collider, out Character character))
        {
            if (IsValidTarget(character))
            {
                character.Hit(_weapon.GetAttackDamage());
            }
        }
    }

    private bool IsValidTarget(Character character)
    {
        return !character.IsDead() && !character.GetType().Equals(_weapon.GetOwner().GetType());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHitBox : CustomMonoBehaviour, IHittable
{
    [SerializeField]
    private Character _character;

    public bool IsHit(Character owner, float damage)
    {
        return _character.IsHit(owner, damage);
    }
}

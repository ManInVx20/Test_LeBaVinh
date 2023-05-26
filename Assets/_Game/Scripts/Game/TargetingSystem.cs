using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class TargetingSystem : CustomMonoBehaviour
{
    [SerializeField]
    private Character _character;
    [SerializeField]
    private float _range = 5.0f;

    private void Update()
    {
        ScanRangedTarget();
    }

    private void ScanRangedTarget()
    {
        Character target = null;

        Collider2D[] colliderArray = Physics2D.OverlapCircleAll(_character.GetTransform().position, _range);
        if (colliderArray.Length > 0)
        {
            for (int i = 0; i < colliderArray.Length; i++)
            {
                if (Cache.TryGetCachedComponent<Character>(colliderArray[i], out Character character) && IsValidTarget(character) &&
                    (target == null || Vector3.Distance(_character.GetTransform().position, target.GetTransform().position) >
                    Vector3.Distance(_character.GetTransform().position, character.GetTransform().position)))
                {
                    target = character;
                }
            }
        }

        _character.SetTarget(target);
    }

    private bool IsValidTarget(Character character)
    {
        return !character.IsDead() && !character.GetType().Equals(_character.GetType());
    }
}

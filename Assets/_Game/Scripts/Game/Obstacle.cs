using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : CustomMonoBehaviour, IHittable
{
    public bool IsHit(Character owner, float damage)
    {
        return true;
    }
}

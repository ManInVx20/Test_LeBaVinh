using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHittable 
{
    bool IsHit(Character owner, float damage);
}

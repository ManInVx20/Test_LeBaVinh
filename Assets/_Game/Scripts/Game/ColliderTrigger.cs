using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTrigger : CustomMonoBehaviour
{
    public event EventHandler OnPlayerEnterTriggerCollider;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (Cache.TryGetCachedComponent<Player>(collider, out _))
        {
            OnPlayerEnterTriggerCollider?.Invoke(this, EventArgs.Empty);
        }
    }
}

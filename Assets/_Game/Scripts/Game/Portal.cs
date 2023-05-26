using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : CustomMonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (Cache.TryGetCachedComponent<Player>(collider, out _))
        {
            Finish();
        }
    }

    private void Finish()
    {
        Debug.Log("Finish");
    }
}

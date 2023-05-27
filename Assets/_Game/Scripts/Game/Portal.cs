using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : CustomMonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider is BoxCollider2D && Cache.TryGetCachedComponent<Player>(collider, out _))
        {
            Finish();
        }
    }

    private void Finish()
    {
        if (LevelManager.Instance.TryLoadNextLevel())
        {
            GameManager.Instance.StartGame();
        }
        else
        {
            GameManager.Instance.ExitGame();
        }
    }
}

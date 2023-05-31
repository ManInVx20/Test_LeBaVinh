using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : CustomMonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (Cache.TryGetCachedComponent<Character>(collider, out Character character) && character is Player)
        {
            Finish();
        }
    }

    private void Finish()
    {
        if (LevelManager.Instance.TryLoadNextLevel())
        {
            Player.Instance.ResetState();

            GameManager.Instance.StartGame();
        }
        else
        {
            GameManager.Instance.ExitGame();
        }
    }
}

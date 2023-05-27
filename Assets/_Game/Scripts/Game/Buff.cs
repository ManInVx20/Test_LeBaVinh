using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : CustomMonoBehaviour, ICollectable
{
    [field: SerializeField]
    public int Price { get; set; }

    [System.Serializable]
    private enum Type
    {
        HaxHealth = 0,
        MoveSpeed = 1,
    }

    [SerializeField]
    private Type _type;

    public void Collect()
    {
        switch (_type)
        {
            case Type.HaxHealth:
                Player.Instance.ChangeMaxHealth(50.0f);

                break;
            case Type.MoveSpeed:
                Player.Instance.ChangeMoveSpeed(2.0f);

                break;
        }

        Despawn();
    }

    public void Despawn()
    {
        Destroy(gameObject);
    }
}

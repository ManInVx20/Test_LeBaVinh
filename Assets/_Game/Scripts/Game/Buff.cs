using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : CustomMonoBehaviour, ICollectable
{
    [field: SerializeField]
    public int Price { get; set; }
    [field: SerializeField]
    public Sprite Sprite { get; private set; }

    [System.Serializable]
    private enum Type
    {
        MoveSpeed = 0,
        HaxHealth = 1,
        MaxShield = 2,
    }

    [SerializeField]
    private Type _type;

    public void Collect()
    {
        switch (_type)
        {
            case Type.MoveSpeed:
                Player.Instance.ChangeMoveSpeed(2.0f);

                break;
            case Type.HaxHealth:
                Player.Instance.ChangeMaxHealth(50.0f);

                break;
            case Type.MaxShield:
                Player.Instance.ChangeMaxShield(10.0f);

                break;
        }

        Despawn();
    }

    public void Despawn()
    {
        Destroy(gameObject);
    }
}

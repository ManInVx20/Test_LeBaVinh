using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Level : CustomMonoBehaviour
{
    [SerializeField]
    private Transform _startPoint;

    private void Start()
    {
        Player.Instance.GetTransform().position = _startPoint.position;
    }

    public void Despawn()
    {
        Destroy(gameObject);
    }
}

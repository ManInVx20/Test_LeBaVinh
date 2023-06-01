using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Level : CustomMonoBehaviour
{
    public Vector3 StartPosition => _startPoint.position;

    [SerializeField]
    private Transform _startPoint;

    public void Despawn()
    {
        Destroy(gameObject);
    }
}

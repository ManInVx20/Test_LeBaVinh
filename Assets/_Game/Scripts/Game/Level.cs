using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : CustomMonoBehaviour
{
    [SerializeField]
    private Transform _startPoint;

    private void Start()
    {
        Player.Instance.GetTransform().position = _startPoint.position;
    }
}

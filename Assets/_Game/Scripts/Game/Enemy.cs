using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : Character
{
    [System.Serializable]
    public enum Type
    {
        Zombie = 0,
        Witch =1,
    }

    public IEnemyState State { get; private set; }

    [field: Header("Prize")]
    [field: SerializeField]
    public float EnergyPrize { get; private set; } = 10.0f;
    [field: SerializeField]
    public int GoldPrize { get; private set; } = 1;

    [Header("Other")]
    [SerializeField]
    private Type _type;
    [SerializeField]
    private LayerMask _obstacleLayerMask;

    public void ChangeState(IEnemyState newState)
    {
        State?.Exit(this);

        State = newState;

        State?.Enter(this);
    }

    public bool HasObstacle(Vector3 direction)
    {
        float distance = 1.0f;
        RaycastHit2D raycastHit = Physics2D.Raycast(GetTransform().position, direction, distance, _obstacleLayerMask);
        if (raycastHit.collider != null)
        {
            return true;
        }

        return false;
    }
}

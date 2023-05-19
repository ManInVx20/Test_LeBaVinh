using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public static Player Instance { get; private set; }

    private Joystick _joystick;
    private List<ICollectable> _collectableItemList = new List<ICollectable>();

    public override void Initialize()
    {
        Instance = this;

        base.Initialize();

        _joystick = FindObjectOfType<Joystick>();
    }

    public override void Execute()
    {
        base.Execute();

        Vector3 moveDirection = Vector3.zero;

        //// PC (Test)
        //moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0.0f).normalized;

        // Mobile
        Vector2 moveInput = _joystick.GetInput().normalized;
        moveDirection = new Vector3(moveInput.x, moveInput.y, 0.0f).normalized;

        SetMoveDirection(moveDirection);
    }

    public override void EnterCollision(Collider2D collider)
    {
        base.EnterCollision(collider);

        if (collider.TryGetComponent<ICollectable>(out ICollectable collectableItem))
        {
            if (!_collectableItemList.Contains(collectableItem))
            {
                _collectableItemList.Add(collectableItem);
            }
        }
    }

    public override void ExitCollision(Collider2D collider)
    {
        base.ExitCollision(collider);

        if (collider.TryGetComponent<ICollectable>(out ICollectable collectableItem))
        {
            if (_collectableItemList.Contains(collectableItem))
            {
                _collectableItemList.Remove(collectableItem);
            }
        }
    }

    public void CollectItem()
    {
        if (_collectableItemList.Count > 0)
        {
            _collectableItemList[0].Collect();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private Joystick _joystick;

    public override void Initialize()
    {
        base.Initialize();

        _joystick = FindObjectOfType<Joystick>();
    }

    public override void Execute()
    {
        base.Execute();

        Vector3 moveDirection = Vector3.zero;

        // PC (Test)
        //moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0.0f).normalized;

        //// Mobile
        Vector2 moveInput = _joystick.GetInput().normalized;
        moveDirection = new Vector3(moveInput.x, moveInput.y, 0.0f).normalized;

        SetMoveDirection(moveDirection);
    }
}

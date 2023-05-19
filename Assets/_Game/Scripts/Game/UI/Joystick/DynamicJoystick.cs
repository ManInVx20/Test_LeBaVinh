using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DynamicJoystick : Joystick
{
    [SerializeField]
    private bool _showWhenPressed = true;
    [SerializeField]
    private bool _hideWhenReleased = true;

    private void Start()
    {
        if (_hideWhenReleased)
        {
            HideVirtualControls();
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        _joystickImage.rectTransform.position = eventData.position;

        base.OnPointerDown(eventData);

        if (_showWhenPressed)
        {
            ShowVirtualControls();
        }
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);

        if (_hideWhenReleased)
        {
            HideVirtualControls();
        }
    }

    private void ShowVirtualControls()
    {
        _joystickImage.gameObject.SetActive(true);
    }

    private void HideVirtualControls()
    {
        _joystickImage.gameObject.SetActive(false);
    }
}

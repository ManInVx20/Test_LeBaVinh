using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HoldButton : CustomMonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent OnHoldButtonDown;
    public UnityEvent OnHoldButtonUp;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnHoldButtonDown?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnHoldButtonUp?.Invoke();
    }
}

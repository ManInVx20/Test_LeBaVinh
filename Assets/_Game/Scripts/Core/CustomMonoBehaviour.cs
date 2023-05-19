using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomMonoBehaviour : MonoBehaviour
{
    private Transform _transform;

    public Transform GetTransform()
    {
        if (_transform == null)
        {
            _transform = GetComponent<Transform>();
        }

        return _transform;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}

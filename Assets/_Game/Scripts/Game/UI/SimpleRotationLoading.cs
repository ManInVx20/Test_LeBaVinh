using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotationLoading : CustomMonoBehaviour
{
    [SerializeField]
    private RectTransform _iconRectTranform;
    [SerializeField]
    private float _timeStep;
    [SerializeField]
    private float _oneStepAngle;

    private float _startTime;

    private void Start()
    {
        _startTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - _startTime >= _timeStep)
        {
            _startTime = Time.time;

            Vector3 iconAngle = _iconRectTranform.localEulerAngles;
            iconAngle.z += _oneStepAngle;
            _iconRectTranform.localEulerAngles = iconAngle;
        }
    }
}

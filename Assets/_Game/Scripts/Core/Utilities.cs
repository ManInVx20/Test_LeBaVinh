using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    public static Vector3 GetRandomHorizontalDirection()
    {
        float randomX = UnityEngine.Random.Range(-1.0f, 1.0f);
        float randomZ = UnityEngine.Random.Range(-1.0f, 1.0f);

        return new Vector3(randomX, 0.0f, randomZ).normalized;
    }

    public static Camera GetCameraWithName(string name)
    {
        for (int i = 0; i < Camera.allCamerasCount; i++)
        {
            if (Camera.allCameras[i].gameObject.name == name)
            {
                return Camera.allCameras[i];
            }
        }

        return null;
    }

    public static IEnumerator DelayActionCoroutine(float delayTime, Action action)
    {
        yield return new WaitForSeconds(delayTime);

        action?.Invoke();
    }

    public static IEnumerator DelayActionRealtimeCoroutine(float delayTime, Action action)
    {
        yield return new WaitForSecondsRealtime(delayTime);

        action?.Invoke();
    }

    public static float GetAngleFromDirection(Vector3 direction)
    {
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (angle < 0.0f)
        {
            angle += 360.0f;
        }

        return angle;
    }
}

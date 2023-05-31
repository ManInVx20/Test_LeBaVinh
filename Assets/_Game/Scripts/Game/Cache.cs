using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Cache
{
    private static Dictionary<Collider, Component> _componentDict = new Dictionary<Collider, Component>();
    private static Dictionary<Collider2D, Component> _component2DDict = new Dictionary<Collider2D, Component>();
    private static Dictionary<Collider2D, IHittable> _hittableDict = new Dictionary<Collider2D, IHittable>();

    public static bool TryGetCachedComponent<T>(Collider collider, out T component) where T : Component
    {
        if (_componentDict.TryGetValue(collider, out Component value) && value is T)
        {
            component = value as T;

            return true;
        }
        else if (collider.TryGetComponent<T>(out component))
        {
            _componentDict[collider] = component;

            return true;
        }

        component = null;

        return false;
    }

    public static bool TryGetCachedComponent<T>(Collider2D collider, out T component) where T : Component
    {
        if (_component2DDict.TryGetValue(collider, out Component value) && value is T)
        {
            component = value as T;

            return true;
        }
        else if (collider.TryGetComponent<T>(out component))
        {
            _component2DDict[collider] = component;

            return true;
        }

        component = null;

        return false;
    }

    public static bool TryGetHittableComponent2D(Collider2D collider, out IHittable component)
    {
        if (_hittableDict.TryGetValue(collider, out component))
        {
            return true;
        }
        else if (collider.TryGetComponent<IHittable>(out component))
        {
            _hittableDict[collider] = component;

            return true;
        }

        return false;
    }
}

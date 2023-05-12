using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour, IObjectPool<T> where T : MonoBehaviour, IPoolable
{
    [SerializeField]
    private T _prefab;

    private List<T> _allInstances = new List<T>();
    private Stack<T> _reusableInstances = new Stack<T>();

    public T GetPrefabInstance()
    {
        T instance;

        if (_reusableInstances.Count > 0)
        {
            instance = _reusableInstances.Pop();

            instance.transform.SetParent(null);

            instance.transform.localPosition = Vector3.zero;
            instance.transform.localEulerAngles = Vector3.zero;
            instance.transform.localScale = Vector3.one;

            instance.gameObject.SetActive(true);
        }
        else
        {
            instance = Instantiate(_prefab);

            _allInstances.Add(instance);
        }

        instance.Origin = this;
        instance.PrepareToUse();

        return instance;
    }

    public void ReturnToPool(T instance)
    {
        instance.gameObject.SetActive(false);

        instance.transform.SetParent(transform);

        instance.transform.localPosition = Vector3.zero;
        instance.transform.localEulerAngles = Vector3.zero;
        instance.transform.localScale = Vector3.one;

        _reusableInstances.Push(instance);
    }

    public void ReturnToPool(object instance)
    {
        if (instance is T)
        {
            ReturnToPool(instance as T);
        }
    }

    public List<T> GetAllInstances()
    {
        return _allInstances;
    }

    public void DestroyAllInstances()
    {
        for (int i = _allInstances.Count - 1; i >= 0; i--)
        {
            if (_allInstances[i] != null)
            {
                _allInstances[i].ReturnToPool();

                Destroy(_allInstances[i].gameObject);
            }

            _allInstances.RemoveAt(i);
        }
    }
}

public interface IObjectPool
{
    void ReturnToPool(object instance);
    bool Exist();
}

public interface IObjectPool<T> : IObjectPool where T : IPoolable
{
    T GetPrefabInstance();
    void ReturnToPool(T instance);
}
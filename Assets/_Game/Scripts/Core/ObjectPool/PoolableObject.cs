using UnityEngine;

public class PoolableObject : CustomMonoBehaviour, IPoolable
{
    public IObjectPool Origin { get; set; }

    public virtual void PrepareToUse()
    {
        
    }

    public virtual void ReturnToPool()
    {
        Origin.ReturnToPool(this);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitVFX : PoolableObject
{
    private ParticleSystem _particleSystem;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    public void Initialize(Transform refTransform)
    {
        GetTransform().position = refTransform.position;
        GetTransform().localScale = refTransform.localScale;

        _particleSystem.Play();

        Invoke(nameof(Despawn), 1.0f);
    }

    public void Despawn()
    {
        ReturnToPool();
    }
}

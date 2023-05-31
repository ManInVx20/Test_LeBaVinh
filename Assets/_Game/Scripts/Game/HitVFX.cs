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

    private void OnParticleSystemStopped()
    {
        ReturnToPool();
    }

    public void Initialize(Transform refTransform)
    {
        GetTransform().position = refTransform.position;
        GetTransform().localScale = refTransform.localScale;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PoolableObject
{
    [SerializeField]
    private float _flySpeed = 10.0f;
    [SerializeField]
    private float _despawnTime = 3.0f;

    private Rigidbody2D _rb;
    private float _despawnTimer;
    private Character _owner;
    private float _damage;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _despawnTimer += Time.deltaTime;
        if (_despawnTimer >= _despawnTime)
        {
            Despawn();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!gameObject.activeInHierarchy)
        {
            return;
        }

        if (collider.GetType().Equals(typeof(BoxCollider2D)))
        {
            if (Cache.TryGetCachedComponent<Character>(collider, out Character character) && IsValidTarget(character))
            {
                character.Hit(_damage);

                Despawn();
            }
        }

        if (Cache.TryGetCachedComponent<Obstacle>(collider, out _))
        {
            Despawn();
        }
    }

    public void Initialize(Vector3 position, Vector3 eulerAngles, Character owner, float damage)
    {
        GetTransform().position = position;
        GetTransform().eulerAngles = eulerAngles + GetTransform().eulerAngles;
        _owner = owner;
        _damage = damage;

        _rb.velocity = GetTransform().right * _flySpeed;
        _despawnTimer = 0.0f;
    }

    private void Despawn()
    {
        if (Origin.Exist())
        {
            ReturnToPool();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private bool IsValidTarget(Character character)
    {
        return !character.IsDead() && !character.GetType().Equals(_owner.GetType());
    }
}

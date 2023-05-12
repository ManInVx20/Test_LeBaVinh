using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float _flySpeed = 10.0f;
    [SerializeField]
    private float _despawnTime = 3.0f;
     
    private Transform _transform;
    private Rigidbody2D _rb;
    private float _despawnTimer;
    private Character _owner;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
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
        if (collider.GetType().Equals(typeof(BoxCollider2D)) && Cache.TryGetCachedComponent2D<Character>(collider, out Character character))
        {
            if (IsValidTarget(character))
            {
                character.Hit(10.0f);

                Despawn();
            }
        }
    }

    public void Initialize(Vector3 position, Vector3 eulerAngles, Character owner)
    {
        _transform.position = position;
        _transform.eulerAngles = eulerAngles + _transform.eulerAngles;
        _owner = owner;

        _rb.velocity = _transform.up * _flySpeed;
    }

    private void Despawn()
    {
        Destroy(gameObject);
    }

    private bool IsValidTarget(Character character)
    {
        return !character.IsDead() && !character.GetType().Equals(_owner.GetType());
    }
}

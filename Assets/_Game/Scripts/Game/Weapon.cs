using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : CustomMonoBehaviour, ICollectable
{
    [SerializeField]
    private float _consumedEnergy = 1.0f;
    [SerializeField]
    private float _bulletDamage = 10.0f;
    [SerializeField]
    private float _manualAttackTime = 0.5f;
    [SerializeField]
    private AnimationCurve _autoAttackTimeAnimationCurve;

    private Animator _animator;
    private Character _owner;
    private int _attackHash;
    private float _attackTimer;
    private float _attackTime;

    private void Awake()
    {
        Initialize();
    }

    private void OnDisable()
    {
        UpdateAttackTime();

        _attackTimer = _attackTime;
    }

    private void Start()
    {
        Begin();
    }

    private void Update()
    {
        Execute();
    }

    public virtual void Initialize()
    {
        _animator = GetComponent<Animator>();
        _attackHash = Animator.StringToHash("Attack");
    }

    public virtual void Begin()
    {
        UpdateAttackTime();

        _attackTimer = _attackTime;
    }

    public virtual void Execute()
    {
        if (_owner == null || _owner.IsDead())
        {
            return;
        }

        UpdateAttackTime();

        _attackTimer += Time.deltaTime;
        if (_attackTimer >= _attackTime)
        {
            _attackTimer = _attackTime;

            if (_owner.IsManualAttacking() || _owner.IsAutoAttacking())
            {
                if (!_owner.UseEnergy())
                {
                    _attackTimer = 0.0f;

                    Attack();
                }
                else if (_owner.GetEnergy() >= _consumedEnergy)
                {
                    _attackTimer = 0.0f;

                    Attack();

                    _owner.ChangeEnergy(-_consumedEnergy);
                }
            }
        }
    }

    public virtual void Attack()
    {
        _animator.SetTrigger(_attackHash);
    }

    public void Initialize(Transform parent, Character owner)
    {
        GetTransform().SetParent(parent);
        GetTransform().localPosition = Vector3.zero;
        GetTransform().localRotation = Quaternion.identity;
        GetTransform().localScale = Vector3.one;

        _owner = owner;

        UpdateAttackTime();

        _attackTimer = _attackTime;
    }

    public void Despawn()
    {
        Destroy(gameObject);
    }

    public void Drop(Vector3 position)
    {
        GetTransform().SetParent(null);
        GetTransform().localPosition = position;
        GetTransform().localRotation = Quaternion.identity;
        GetTransform().localScale = Vector3.one;

        _owner = null;

        Show();
    }

    public Character GetOwner()
    {
        return _owner;
    }

    public float GetAttackDamage()
    {
        return _bulletDamage;
    }

    public virtual void Collect()
    {
        
    }

    private void UpdateAttackTime()
    {
        if (_owner == null || _owner.IsDead())
        {
            return;
        }

        if (!_owner.IsAutoAttacking())
        {
            _attackTime = _manualAttackTime;
        }
        else
        {
            _attackTime = _autoAttackTimeAnimationCurve.Evaluate(_owner.GetEnergy() / _owner.GetMaxEnergy());
        }
    }
}

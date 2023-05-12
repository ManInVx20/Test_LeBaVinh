using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Character : MonoBehaviour
{
    private const float MIN_MOVE_SQR_MAGNITUDE = 0.01f;

    [Header("Components")]
    [SerializeField]
    private CharacterAnimator _characterAnimator;

    [Header("Properties")]
    [SerializeField]
    private bool _isFacingRight = true;
    [SerializeField]
    private float _moveSpeed = 5.0f;
    [SerializeField]
    private float _maxHealth = 100.0f;
    [SerializeField]
    private float _maxEnergy = 150.0f;
    [SerializeField]
    private float _maxShield = 20.0f;

    private Transform _transform;
    private Rigidbody2D _rb;
    private Vector3 _moveDirection;
    private Character _target;
    private Vector3 _aimDirection;
    private Weapon _weapon;
    private float _health;

    #region UNITY_METHODS

    private void Awake()
    {
        Initialize();
    }

    private void Start()
    {
        Begin();
    }

    private void Update()
    {
        Execute();
    }

    private void FixedUpdate()
    {
        FixedExecute();
    }

    #endregion

    public virtual void Initialize()
    {
        _transform = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody2D>();

        _weapon = GetComponentInChildren<Weapon>();
        _weapon.Initialize(this);
    }

    public virtual void Begin()
    {
        _health = _maxHealth;

        if (_isFacingRight)
        {
            _transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }
        else
        {
            _transform.localRotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }
    }

    public virtual void Execute()
    {
        if (IsDead())
        {
            return;
        }

        HandleLook();
        HandleAim();
        HandleAnim();
    }

    public virtual void FixedExecute()
    {
        _rb.velocity = _moveDirection * _moveSpeed;
    }

    public Transform GetTransform()
    {
        return _transform;
    }

    public CharacterAnimator GetCharacterAnimator()
    {
        return _characterAnimator;
    }

    public bool IsDead()
    {
        return _health <= 0.0f;
    }

    public void SetMoveDirection(Vector3 moveDirection)
    {
        _moveDirection = moveDirection;
    }

    public void SetTarget(Character target)
    {
        _target = target;
    }

    public void Flip()
    {
        _isFacingRight = !_isFacingRight;
        _transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    public void Attack()
    {
        _weapon.ShootBullet();
    }

    public void Hit(float damage)
    {
        if (IsDead())
        {
            return;
        }

        _health -= damage;

        if (IsDead())
        {
            Die();
        }
    }

    public void Die()
    {
        _characterAnimator.ChangeAnim(CharacterAnimator.Anim.Die);

        Invoke(nameof(Despawn), 2.0f);
    }

    public void Despawn()
    {
        Destroy(gameObject);
    }

    private void HandleLook()
    {
        if (_target == null)
        {
            if (_moveDirection.x < 0.0f && _isFacingRight)
            {
                Flip();
            }
            else if (_moveDirection.x > 0.0f && !_isFacingRight)
            {
                Flip();
            }
        }
        else
        {
            if (_transform.position.x > _target.GetTransform().position.x && _isFacingRight)
            {
                Flip();
            }
            else if (_transform.position.x < _target.GetTransform().position.x && !_isFacingRight)
            {
                Flip();
            }
        }
    }

    private void HandleAim()
    {
        if (_target == null)
        {
            if (_moveDirection.sqrMagnitude >= MIN_MOVE_SQR_MAGNITUDE)
            {
                _aimDirection = _transform.localRotation * _moveDirection;
            }
        }
        else
        {
            _aimDirection = _transform.localRotation * (_target.GetTransform().position - _transform.position).normalized;
        }

        _weapon.GetTransform().localEulerAngles = new Vector3(0.0f, 0.0f, Utilities.GetAngleFromDirection(_aimDirection));
    }

    private void HandleAnim()
    {
        if (_moveDirection.sqrMagnitude >= MIN_MOVE_SQR_MAGNITUDE)
        {
            _characterAnimator.ChangeAnim(CharacterAnimator.Anim.Run);
        }
        else
        {
            _characterAnimator.ChangeAnim(CharacterAnimator.Anim.Idle);
        }
    }
}

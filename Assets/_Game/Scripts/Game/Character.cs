using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : CustomMonoBehaviour
{
    public class OnCharacterHealthChangedArgs : EventArgs
    {
        public float Health;
        public float MaxHealth;
    }
    public event EventHandler<OnCharacterHealthChangedArgs> OnCharacterHealthChanged;
    public class OnCharacterEnergyChangedArgs : EventArgs
    {
        public float Energy;
        public float MaxEnergy;
    }
    public event EventHandler<OnCharacterEnergyChangedArgs> OnCharacterEnergyChanged;
    public class OnCharacterShieldChangedArgs : EventArgs
    {
        public float Shield;
        public float MaxShield;
    }
    public event EventHandler<OnCharacterShieldChangedArgs> OnCharacterShieldChanged;

    private const float MIN_MOVE_SQR_MAGNITUDE = 0.01f;

    [Header("Components")]
    [SerializeField]
    private CharacterAnimator _characterAnimator;
    [SerializeField]
    private Transform _mainWeaponHolder;
    [SerializeField]
    private Transform _subWeaponHolder;

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
    [SerializeField]
    private float _restoreShieldTime = 2.0f;
    [SerializeField]
    private float _restoreShieldValue = 5.0f;

    private Rigidbody2D _rb;
    private Vector3 _moveDirection;
    private Character _target;
    private Vector3 _aimDirection;
    private Weapon _mainWeapon;
    private Weapon _subWeapon;
    private float _health;
    private float _energy;
    private float _shield;
    private float _restoreShieldTimer;
    private bool _isMainWeapon;
    private bool _isManualAttacking;
    private bool _isAutoAttacking;

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

    private void OnTriggerEnter2D(Collider2D collider)
    {
        EnterCollision(collider);
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        ExitCollision(collider);
    }

    #endregion

    public virtual void Initialize()
    {
        _rb = GetComponent<Rigidbody2D>();

        SwapMainWeapon(_mainWeaponHolder.GetComponentInChildren<Weapon>());

        _subWeapon = _subWeaponHolder.GetComponentInChildren<Weapon>();
        _subWeapon.Initialize(_subWeaponHolder, this);
    }

    public virtual void Begin()
    {
        _rb.simulated = true;
        _health = _maxHealth;
        _energy = _maxEnergy;
        _shield = _maxShield;

        if (_isFacingRight)
        {
            GetTransform().localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }
        else
        {
            GetTransform().localRotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }

        _isMainWeapon = true;

        if (_isMainWeapon)
        {
            _mainWeapon.Show();
            _subWeapon.Hide();
        }
        else
        {
            _mainWeapon.Hide();
            _subWeapon.Show();
        }

        OnCharacterHealthChanged?.Invoke(this, new OnCharacterHealthChangedArgs
        {
            Health = _health,
            MaxHealth = _maxHealth
        });

        OnCharacterEnergyChanged?.Invoke(this, new OnCharacterEnergyChangedArgs
        {
            Energy = _energy,
            MaxEnergy = _maxEnergy
        });

        OnCharacterShieldChanged?.Invoke(this, new OnCharacterShieldChangedArgs
        {
            Shield = _shield,
            MaxShield = _maxShield
        });
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
        HandleRestoreShield();
    }

    public virtual void FixedExecute()
    {
        _rb.velocity = _moveDirection * _moveSpeed;
    }

    public virtual void EnterCollision(Collider2D collider)
    {

    }

    public virtual void ExitCollision(Collider2D collider)
    {

    }

    public CharacterAnimator GetCharacterAnimator()
    {
        return _characterAnimator;
    }

    public bool IsDead()
    {
        return _health <= 0.0f;
    }

    public bool IsMainWeapon()
    {
        return _isMainWeapon;
    }

    public bool IsManualAttacking()
    {
        return _isManualAttacking;
    }

    public bool IsAutoAttacking()
    {
        return _isAutoAttacking;
    }

    public bool HasTarget()
    {
        return _target != null;
    }

    public float GetMaxEnergy()
    {
        return _maxEnergy;
    }

    public float GetEnergy()
    {
        return _energy;
    }

    public bool UseEnergy()
    {
        return _maxEnergy > 0.0f;
    }

    public bool UseShield()
    {
        return _maxShield > 0.0f;
    }

    public void SetMoveDirection(Vector3 moveDirection)
    {
        _moveDirection = moveDirection;
    }

    public void SetTarget(Character target)
    {
        _target = target;
    }

    public void ChangeEnergy(float value)
    {
        _energy += value;
        if (_energy < 0.0f)
        {
            _energy = 0.0f;
        }
        else if (_energy > _maxEnergy)
        {
            _energy = _maxEnergy;
        }

        OnCharacterEnergyChanged?.Invoke(this, new OnCharacterEnergyChangedArgs
        {
            Energy = _energy,
            MaxEnergy = _maxEnergy
        });
    }

    public void Flip()
    {
        _isFacingRight = !_isFacingRight;
        GetTransform().Rotate(0.0f, 180.0f, 0.0f);
    }

    public void SwapMainWeapon(Weapon weapon)
    {
        if (_mainWeapon != null)
        {
            _mainWeapon.Drop(weapon.GetTransform().position);
        }

        if (weapon != null)
        {
            _mainWeapon = weapon;
            _mainWeapon.Initialize(_mainWeaponHolder, this);

            if (!_isMainWeapon)
            {
                _mainWeapon.Hide();
            }
        }
    }

    public void SwapWeapon()
    {
        _isMainWeapon = !_isMainWeapon;

        if (_isMainWeapon)
        {
            _mainWeapon.Show();
            _subWeapon.Hide();
        }
        else
        {
            _mainWeapon.Hide();
            _subWeapon.Show();
        }
    }

    public void EnableManualAttack()
    {
        if (_isAutoAttacking)
        {
            return;
        }

        _isManualAttacking = true;
    }

    public void DisableManualAttack()
    {
        _isManualAttacking = false;
    }

    public void ToggleAutoAttack()
    {
        _isAutoAttacking = !_isAutoAttacking;

        if (_isManualAttacking)
        {
            DisableManualAttack();
        }
    }

    public void Hit(float damage)
    {
        if (IsDead())
        {
            return;
        }

        _restoreShieldTimer = 0.0f;

        float damageToHealth = damage;
        if (_shield > 0.0f)
        {
            _shield -= damage;
            if (_shield < 0.0f)
            {
                damageToHealth = -_shield;

                _shield = 0.0f;
            }
            else
            {
                damageToHealth = 0.0f;
            }

            OnCharacterShieldChanged?.Invoke(this, new OnCharacterShieldChangedArgs
            {
                Shield = _shield,
                MaxShield = _maxShield
            });
        }

        _health -= damageToHealth;
        if (_health < 0.0f)
        {
            _health = 0.0f;
        }

        OnCharacterHealthChanged?.Invoke(this, new OnCharacterHealthChangedArgs
        {
            Health = _health,
            MaxHealth = _maxHealth
        });

        if (IsDead())
        {
            Die();
        }
    }

    public void Die()
    {
        _rb.simulated = false;

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
            if (GetTransform().position.x > _target.GetTransform().position.x && _isFacingRight)
            {
                Flip();
            }
            else if (GetTransform().position.x < _target.GetTransform().position.x && !_isFacingRight)
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
                _aimDirection = GetTransform().localRotation * _moveDirection;
            }
        }
        else
        {
            _aimDirection = GetTransform().localRotation * (_target.GetTransform().position - GetTransform().position).normalized;
        }

        _mainWeapon.GetTransform().localEulerAngles = new Vector3(0.0f, 0.0f, Utilities.GetAngleFromDirection(_aimDirection));

        _subWeapon.GetTransform().localEulerAngles = new Vector3(0.0f, 0.0f, Utilities.GetAngleFromDirection(_aimDirection));
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

    private void HandleRestoreShield()
    {
        if (!UseShield())
        {
            return;
        }

        _restoreShieldTimer += Time.deltaTime;
        if (_restoreShieldTimer >= _restoreShieldTime)
        {
            _restoreShieldTimer = 0.0f;

            _shield += _restoreShieldValue;
            if (_shield > _maxShield)
            {
                _shield = _maxShield;
            }

            OnCharacterShieldChanged?.Invoke(this, new OnCharacterShieldChangedArgs
            {
                Shield = _shield,
                MaxShield = _maxShield
            });
        }
    }
}

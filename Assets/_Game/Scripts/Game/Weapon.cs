using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private Transform _shootPoint;
    [SerializeField]
    private Bullet _bulletPrefab;

    private Transform _transform;
    private Character _owner;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    public Transform GetTransform()
    {
        return _transform;
    }

    public void Initialize(Character owner)
    {
        _owner = owner;
    }

    public void ShootBullet()
    {
        Bullet bullet = Instantiate(_bulletPrefab);
        bullet.Initialize(_shootPoint.position, _shootPoint.eulerAngles, _owner);
    }
}

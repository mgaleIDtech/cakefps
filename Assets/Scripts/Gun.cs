using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private Bullet _bulletPrefab = null;

    [SerializeField]
    private Transform _bulletSpawnPoint = null;

    private float _attackSpeed = 0.75f;
    private float _attackCooldown = 0f;

    /// <summary>
    /// True if gun is cooling down from last attack.
    /// </summary>
    public bool IsOnCooldown { get { return _attackCooldown > 0f; } }

    private void Update()
    {
        HandleAttackCooldown();
    }

    private void HandleAttackCooldown()
    {
        if (_attackCooldown > 0f)
            _attackCooldown -= Time.deltaTime;

        if (_attackCooldown < 0f)
            _attackCooldown = 0f;
    }

    private void SetCooldown()
    {
        _attackCooldown = _attackSpeed;
    }

    public void Shoot()
    {
        if (IsOnCooldown)
            return;

        var bullet = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation);

        SetCooldown();
    }
}

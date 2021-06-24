using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField]
    private Bullet _bulletPrefab = null;

    [SerializeField]
    private Transform _bulletSpawnPoint = null;


    public void Shoot()
    {
        var bullet = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation);
    }
}

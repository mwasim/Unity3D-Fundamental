using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    private Transform _firePoint;

    [SerializeField]
    private GameObject _bulletPrefab;

    private float _bulletForce = 20f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        var bullet = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);

        //add force to the bullet
        var rbBullet = bullet.GetComponent<Rigidbody2D>();

        //in the firePoint up direction with bullet force
        rbBullet.AddForce(_firePoint.up * _bulletForce, ForceMode2D.Impulse);

        Destroy(bullet, 0.5f); //destroy after half a second
    }
}

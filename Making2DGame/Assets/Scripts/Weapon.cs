using UnityEngine;

/*
    Weapon.cs (this script) uses Bullet Prefab for shooting
    In the Unity Editor, we can use either WeaponRaycast.cs (for raycast shooting) or Weapon.cs script (shooting using bullet prefab)
 */
public class Weapon : MonoBehaviour
{
    [SerializeField]
    private Transform _firePoint;

    [SerializeField]
    private GameObject _bulletPrefab;

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
        //Shooting logic
        Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);

    }
}

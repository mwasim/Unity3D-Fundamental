using System.Collections;
using UnityEngine;

/*
    WeaponRaycast uses raycast for shooting (just like it's done in some famous FPS games)
    In the Unity Editor, we can use either WeaponRaycast.cs (for raycast shooting) or Weapon.cs script (shooting using bullet prefab)
 */
public class WeaponRaycast : MonoBehaviour
{
    [SerializeField]
    private Transform _firePoint;

    [Header("Enemy Settings")]
    [SerializeField]
    private int _damageLevel = 40;

    [SerializeField]
    private GameObject _shotImpactEffect;

    [SerializeField]
    private LineRenderer _lineRenderer;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(nameof(Shoot));
        }
    }

    private IEnumerator Shoot()
    {
        var hitInfo = Physics2D.Raycast(_firePoint.position, _firePoint.right);

        if (hitInfo)
        {
            //Debug.Log(hitInfo.transform.name);

            var enemy = hitInfo.transform.GetComponent<Enemy>();
            if (enemy != null) //if enemy is hit
            {
                enemy.TakeDamage(_damageLevel);
            }

            var shotImpact = Instantiate(_shotImpactEffect, hitInfo.point, Quaternion.identity);

            Destroy(shotImpact, 0.2f);

            _lineRenderer.SetPosition(0, _firePoint.position); //start position 
            _lineRenderer.SetPosition(1, hitInfo.point); //end position
        }
        else
        {
            //If we don't hit anything, it shouldn't have end position and goe in the space infinitely
            _lineRenderer.SetPosition(0, _firePoint.position);
            _lineRenderer.SetPosition(1, _firePoint.position + _firePoint.right * 100); //take the start position, and shift it 100 units forward
        }

        _lineRenderer.enabled = true;

        //wait for very tiny amount of time (wait for one frame and disable it again, but we need run this function in the coroutine scope
        yield return new WaitForSeconds(0.02f);

        _lineRenderer.enabled = false;
    }
}

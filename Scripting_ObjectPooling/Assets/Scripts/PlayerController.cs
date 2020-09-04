using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject _projectile;
    [SerializeField]
    private float _movmentSpeed = 40.0f;
    
    private GameObject _currentProjectile;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //var projectile = Instantiate(_projectile); //TODO: Instead of instantiating, reuse the projectiles

            _currentProjectile = PoolManager.Instance.AvailableProjectile; //reuse the existing available projectile

            if (_currentProjectile != null) Debug.Log("Available projectile is valid"); else Debug.Log("Available projectile is NULL");
        }

        if (_currentProjectile != null && _currentProjectile.activeInHierarchy) //Unity objects should not use null propagation operator
        {
            var translation = Vector3.forward * _movmentSpeed * Time.deltaTime;
            Debug.Log("Moving projectile at speed: " + translation);
            _currentProjectile.transform.Translate(translation);
        }
    }
}

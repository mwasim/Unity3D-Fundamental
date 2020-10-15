using UnityEngine;

public class PlayerShipController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    
    private Transform myTransform;
  
    // Start is called before the first frame update
    void Start()
    {
        myTransform = transform;

        InvokeRepeating("Shoot", .33f, .33f);
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, myTransform.position, Quaternion.identity);
    }
}

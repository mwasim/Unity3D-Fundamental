using UnityEngine;

public class PlayerShipController : MonoBehaviour
{
    //[SerializeField] private GameObject bulletPrefab; //Now being handled by the Object pool

    private ObjectPool _objectPool;

    private Transform myTransform;
  
    // Start is called before the first frame update
    void Start()
    {
        myTransform = transform;

        _objectPool = GetComponent<ObjectPool>(); //cache a reference

        InvokeRepeating(nameof(Shoot), .33f, .33f);
    }

    void Shoot()
    {
        //Instantiate(bulletPrefab, myTransform.position, Quaternion.identity);

        //Using object pooling technique
        GameObject bullet = _objectPool.GetAvailableObject();
        bullet.transform.position = myTransform.position;
        bullet.SetActive(true);
    }
}

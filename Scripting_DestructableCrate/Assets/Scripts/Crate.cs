using UnityEngine;

public class Crate : MonoBehaviour
{
    public GameObject fracturedCrate;
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var fracturedObject = Instantiate(fracturedCrate, transform.position, Quaternion.identity);

            var rigidBodyList = fracturedObject.GetComponentsInChildren<Rigidbody>();

            if(rigidBodyList.Length > 0)
            {
                foreach(var rb in rigidBodyList)
                {
                    rb.AddExplosionForce(500, transform.position, 1);
                }
            }

            Destroy(gameObject);
        }
    }
}

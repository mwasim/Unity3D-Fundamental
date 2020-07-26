using UnityEngine;

public class SpinPropeller : MonoBehaviour
{
    public float propellerRotationSpeed = 20;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Rotates the propellor on the z axis
        transform.Rotate(Vector3.forward * propellerRotationSpeed * Time.deltaTime);
    }
}

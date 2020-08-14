using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public float rotationSpeed;
    public GameObject foodProjectile;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(foodProjectile, transform.position, transform.rotation);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Rotate(-Vector3.up * rotationSpeed * Time.deltaTime);
        }
    }
}

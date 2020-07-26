using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float topSpeed = 10;
    public float turnSpeed = 5;

    private float horizontalInput;
    private float forwardInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        //Move the vehicle forward
        //transform.Translate(0, 0, 0.1f);
        //above line can be written as, Vector3.forward = (0, 0, 1), Z = 1
        transform.Translate(Vector3.forward * Time.deltaTime * topSpeed * forwardInput);

        //This code moves the vehicle left or right
        //Vector3.up = (1, 0, 0), X = 1
        //transform.Translate(Vector3.right * Time.deltaTime * turnSpeed * horizontalInput);

        //We need to rotate the vehicle left or right
        //Vector3.up = (0, 1, 0), Y = 1
        transform.Rotate(Vector3.up * turnSpeed * horizontalInput * Time.deltaTime);
    }
}

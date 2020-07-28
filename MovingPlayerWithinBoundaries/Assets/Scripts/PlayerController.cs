using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 10.0f;
    public float clampX = 10.0f;

    private float horizontalInput;

    void FixedUpdate()
    {
        //STEP-1: MOVE
        //get horizontal input
        horizontalInput = Input.GetAxis("Horizontal");

        //move left or right
        transform.Translate(Vector3.right * horizontalInput * maxSpeed * Time.deltaTime);


        //STEP-2: CLAMP to ensure the player moves within the boundary (-X to X)
        //Ensure player cannot move past to the clampX position
        if (transform.position.x > clampX)
            transform.position = new Vector3(clampX, transform.position.y, transform.position.z);

        if (transform.position.x < -clampX)
            transform.position = new Vector3(-clampX, transform.position.y, transform.position.z);
    }
}

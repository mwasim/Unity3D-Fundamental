using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Speed Settings")] //header adds a header in the inspector
    //SerializeField attribute allows you to see/edit the private variable in the inspector
    [SerializeField] float speed;

    private float horizontalInput;
    private float horizontalRange = 9.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    //NOTE: It's important do all stuff related to Physics in the FixedUpdate to make everything smooth
    void FixedUpdate()
    {
        //STEP-1: Move the player based on the horizontal input
        MovePlayer();

        //STEP-2: CLAMP to ensure the player moves within the boundary (-X to X)
        CheckBounds();
    }

    public void MovePlayer()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);
    }


    private void CheckBounds()
    {
        //STEP-2: CLAMP to ensure the player moves within the boundary (-X to X)
        //Ensure player cannot move past to the clampX position
        if (transform.position.x >= horizontalRange)
            transform.position = new Vector3(horizontalRange, transform.position.y, transform.position.z);

        if (transform.position.x <= -horizontalRange)
            transform.position = new Vector3(-horizontalRange, transform.position.y, transform.position.z);
    }
}

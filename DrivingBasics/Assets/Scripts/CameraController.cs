using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    private Vector3 offset; //distance between camera and player's position

    // Start is called before the first frame update
    void Start()
    {
        //On the start, we've set the camera offset correctly to the player's position, so let's save that offset
        /*
            For example, at the start we know
            Camera's position = Vector3(0, 5, -10)
            Player's position = Vector3(0, 0, 0)
         */
        offset = transform.position - player.transform.position;

        print($"Offset: ${offset}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //If Camera is little shaking use LateUpdate (it executes after Update is finished)
    private void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}

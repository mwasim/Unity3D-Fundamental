using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;


    // Start is called before the first frame update
    void Start()
    {
        //As the script is attached to the Main Camera, so the transform is attacheed to the camera
        //wee take offset by subtracting player position from the camera possition
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - player.transform.position;    
    }    

    //For follow cameras, procedural animation and gathering last known states, it's best to use LateUpdate
    //Like Update runs every frame but it's guaranteed to run aftr all items have been processed in Update
    //So, whn wee set the position of the player, we alrady know we've already moved the player
    private void LateUpdate()
    {
        //set camera's transform's position
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        transform.position = player.transform.position + offset;
        //Debug.Log("LateUpdate...");
    }
}

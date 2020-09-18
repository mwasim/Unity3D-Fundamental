using UnityEngine;

public class RayViewer : MonoBehaviour
{
    [SerializeField]
    private float _weaponRange = 50f; // Distance in Unity units over which the Debug.DrawRay will be drawn

    private Camera _fpsCamera; // Holds a reference to the first person camera

    // Start is called before the first frame update
    void Start()
    {
        // Get and store a reference to our Camera by searching this GameObject and its parents
        _fpsCamera = GetComponentInParent<Camera>(); //camera in parent as not attached to the current object
    }

    // Update is called once per frame
    void Update()
    {
        // Create a vector at the center of our camera's viewport
        var rayOrigin = _fpsCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0)); //take a point and convert it to the world space

        // Draw a line in the Scene View  from the point lineOrigin in the direction of fpsCam.transform.forward * weaponRange, using the color green
        Debug.DrawRay(rayOrigin, _fpsCamera.transform.forward * _weaponRange, Color.blue);
    }
}

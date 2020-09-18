using System.Collections;
using UnityEngine;

public class RaycastShoot : MonoBehaviour
{
    [SerializeField]
    private int _gunDamage = 1; // Set the number of hitpoints that this gun will take away from shot objects with a health script
    [SerializeField]
    private float _fireRate = .25f; // Number in seconds which controls how often the player can fire
    [SerializeField]
    private float _weaponRange = 50f; // Distance in Unity units over which the player can fire
    [SerializeField]
    private float _hitForce = 100f; // Amount of force which will be added to objects with a rigidbody shot by the player
    [SerializeField]
    private Transform _gunEnd; // Holds a reference to the gun end object, marking the muzzle location of the gun

    private Camera _fpsCamera; // Holds a reference to the first person camera
    private WaitForSeconds _shotDuration = new WaitForSeconds(0.07f); // WaitForSeconds object used by our ShotEffect coroutine, determines time laser line will remain visible
    private AudioSource _gunAudioSource; // Reference to the audio source which will play our shooting sound effect
    private LineRenderer _laserLine; // Reference to the LineRenderer component which will display our laserline
    private float _nextFire; // Float to store the time the player will be allowed to fire again, after firing

    // Start is called before the first frame update
    void Start()
    {
        _laserLine = GetComponent<LineRenderer>(); // Get and store a reference to our LineRenderer component
        _gunAudioSource = GetComponent<AudioSource>(); // Get and store a reference to our AudioSource component
        _fpsCamera = GetComponentInParent<Camera>(); // Get and store a reference to our Camera by searching this GameObject and its parents
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player has pressed the fire button and if enough time has elapsed since they last fired
        if (Input.GetButtonDown("Fire1") && Time.time > _nextFire) //enough time has passed since they last fired
        {
            // Update the time when our player can fire next
            _nextFire = Time.time + _fireRate; //if the time has exceeded our shot frequency, meaning the player can fire once more

            // Start our ShotEffect coroutine to turn our laser line on and off
            StartCoroutine(ShotEffect());

            // Create a vector at the center of our camera's viewport
            //[(0,0) = Bottom Left corner, (1,1) = Top right corner, (0.5,0.5) = CENTER]  - the player aims from
            //always center to the camera, (we achieve this using ViewportToWorldPoint)
            var rayOrigin = _fpsCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0)); //take a point and convert it to the world space

            // Set the start position for our visual effect for our laser to the position of gunEnd
            _laserLine.SetPosition(0, _gunEnd.position); //first position of the line

            // Check if our raycast has hit anything
            if (Physics.Raycast(rayOrigin, _fpsCamera.transform.forward, out RaycastHit hit, _weaponRange))
            {
                // Set the end position for our laser line 
                _laserLine.SetPosition(1, hit.point); //second position of the line (position based on the index - first param)

                // Get a reference to a health script attached to the collider we hit
                var health = hit.collider.GetComponent<ShootableBox>();

                // If there was a health script attached
                if (health != null)
                {
                    // Call the damage function of that script, passing in our gunDamage variable
                    health.Damage(_gunDamage);
                }

                // Check if the object we hit has a rigidbody attached
                if (hit.rigidbody != null) //add force the object hit with opposite direction of normal (minus normal)
                {
                    // Add force to the rigidbody we hit, in the direction from which it was hit
                    hit.rigidbody.AddForce(-hit.normal * _hitForce); //amount of force we need to apply on hitting the object
                }
            }
            else
            {
                // If we did not hit anything, set the end of the line to a position directly in front of the camera at the distance of weaponRange
                //for example we fire in the sky and in that case, raycast didn't hit anything
                _laserLine.SetPosition(1, rayOrigin + (_fpsCamera.transform.forward * _weaponRange)); //set line 50 units away from the origin in the forward direction of the camera
            }

        }
    }


    //turn ON and OFF the laser effect using coroutine
    private IEnumerator ShotEffect()
    {
        _gunAudioSource.Play();// Play the shooting sound effect

        _laserLine.enabled = true;// Turn on our line renderer
        yield return _shotDuration; //wait for shot duration and then disable the laser line
        _laserLine.enabled = false;// Deactivate our line renderer after waiting
    }
}

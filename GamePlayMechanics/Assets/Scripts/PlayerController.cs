using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject focalPoint;

    public float speed;
    public float powerUpForce = 15.0f;
    public bool hasPowerup = false;

    public GameObject PowerupIndicator;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var forwardInput = Input.GetAxis("Vertical");

        //Instead of using the Vector3.forward (global), we're using focalPoint's forward (local)
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed); //Rigidbody doesn't need Time.deltaTime

        //powerup indicator
        PowerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }

    //On colliding the powerup, the powerup should destroy itself (as it's collected or picked up)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);

            hasPowerup = true;
            PowerupIndicator.gameObject.SetActive(true);
            Debug.Log("Powerup destroyed");

            //start coroutine
            StartCoroutine(nameof(PowerupCountdownRoutine));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Debug.Log($"Player collided with: ${collision.gameObject.name}");
            Debug.Log(hasPowerup);

            var enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            var awayFromPlayer = (collision.gameObject.transform.position = transform.position).normalized;

            enemyRb.AddForce(awayFromPlayer * powerUpForce, ForceMode.Impulse); //apply instant force in the direction away from the player
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);

        hasPowerup = false;
        PowerupIndicator.gameObject.SetActive(false);
    }
}

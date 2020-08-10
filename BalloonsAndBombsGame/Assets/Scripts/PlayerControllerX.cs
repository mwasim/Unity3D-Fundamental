using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;

    public float floatForce;
    public float bounceForce;
    private float gravityModifier = 1.5f;
    private Rigidbody playerRb;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;
    public AudioClip bounceSound;

    private readonly Vector3 DefaultGravity = new Vector3(0.0f, -9.8f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();
        playerRb = GetComponent<Rigidbody>();

        Physics.gravity = DefaultGravity * gravityModifier;

        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);

    }

    // Update is called once per frame
    void FixedUpdate()
    {        
        // While space is pressed and player is low enough, float up
        if (Input.GetKey(KeyCode.Space))
        {
            PushBaloonUpwards(floatForce);     
        }
    }

    public void PushBaloonUpwards(float theFloatForce)
    {
        var isLowEnough = transform.position.y <= 18.0f; //Add force only if the balloon is low enough

        // While space is pressed and player is low enough, float up
        if (!gameOver && isLowEnough)
        {
            playerRb.AddForce(Vector3.up * theFloatForce * Time.deltaTime, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);

            //Restart game after 5 seconds
            Invoke(nameof(Restart), 5.0f);
        }

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);

        }
        // if player collides with ground, bounce off the ground and sound
        else if (other.gameObject.CompareTag("Ground") && !gameOver)
        {
            playerAudio.PlayOneShot(bounceSound, 1.0f);

            playerRb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
        }
    }


    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Particle System")]
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;

    [Header("Audio")]
    public AudioClip jumpSound;
    public AudioClip crashSound;

    [Header("Other Settings")]
    public FixedJoystick joystick;
    public float jumpForce = 10.0f;
    public float gravityModifier = 2;
    public bool isGameOver = false;

    private bool isPlayerOnTheGround = false;
    private Animator playerAnimator;
    private AudioSource playerAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        playerAudioSource = GetComponent<AudioSource>();

        //rb.AddForce(Vector3.up * jumpForce); //character jumps upward, and due to gravity the characters returns to the ground
        /*
         * Physics.gravity:
            The gravity applied to all rigid bodies in the Scene.

            //Note: Gravity can be turned off for an individual rigidbody using its useGravity property.
         */
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        var joyStickVertical = joystick.Vertical;
        //if (Input.GetKeyDown(KeyCode.Space) && isPlayerOnTheGround && !isGameOver)
        if (joystick.Vertical > 0.5 && isPlayerOnTheGround && !isGameOver)
        {
            Jump();
        }

    }

    private void Jump()
    {
        /*
         * ForceMode.Impulse:
            Add an instant force impulse to the rigidbody, using its mass.
            https://docs.unity3d.com/ScriptReference/ForceMode.Impulse.html
         */
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        isPlayerOnTheGround = false; //when jumped, the player is no longer on the ground

        playerAnimator.SetTrigger("Jump_trig"); //set jump trigger on Player Animator component to trigger jump animation

        //stop dirt particle system
        dirtParticle.Stop();

        //play jump sound
        playerAudioSource.PlayOneShot(jumpSound, 1.0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider != null && collision.collider.CompareTag("Ground"))
        {
            isPlayerOnTheGround = true;
            dirtParticle.Play();
        }
        else if (collision.collider != null && collision.collider.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over!");
            isGameOver = true;
            //Time.timeScale = 0; //Pauses the game

            playerAnimator.SetBool("Death_b", true);
            playerAnimator.SetInteger("DeathType_int", 1);

            explosionParticle.Play();

            dirtParticle.Stop();

            //play crash sound
            playerAudioSource.PlayOneShot(crashSound, 1.0f);

            Invoke(nameof(RestartGame), 10.0f);
        }
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

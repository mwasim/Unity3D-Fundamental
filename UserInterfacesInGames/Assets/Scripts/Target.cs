using UnityEngine;

public class Target : MonoBehaviour
{
    public float minSpeed = 18f;
    public float maxSpeed = 26f;

    public float maxTorque = 10.0f;

    public float xRange = 4f;
    public float ySpawnPos = -6f;

    public int pointValue;

    public ParticleSystem explosionParticle;

    private Rigidbody targetRb;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();        

        targetRb.AddForce(RandomUpwardForce(), ForceMode.Impulse);
        transform.position = RandomSpawnPos();

        targetRb.AddTorque(RandomTorqueVector3, ForceMode.Impulse);

    }
   
    Vector3 RandomUpwardForce()
    {
        float randomSpeed = Random.Range(minSpeed, maxSpeed);
        return Vector3.up * randomSpeed;
    }

    private float lastXPos;
    Vector3 RandomSpawnPos()
    {
        float randomXPos = Random.Range(-xRange, xRange);

        if(randomXPos == lastXPos)
        {
            randomXPos = Random.Range(-xRange, xRange);
        }

        lastXPos = randomXPos;

        return new Vector3(randomXPos, ySpawnPos);
    }

    //object collides with the Senser
    private void OnTriggerEnter(Collider other)
    {
        //When object is missed and not clicked by mouse, the game should be over
        if (!gameObject.CompareTag("Bad"))
        {
            gameManager.GameOver();
        }

        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        //if game is over, user cannot destory objects
        if (gameManager.isGameOver) return; 

        Destroy(gameObject);

        //each time target is destroyed by mouse click, upate the score
        gameManager.UpdateScore(pointValue);

        Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
    }

    private Vector3 RandomTorqueVector3 => new Vector3(RandomTorqueValue, RandomTorqueValue, RandomTorqueValue);

    private float RandomTorqueValue => Random.Range(0, maxTorque);
}

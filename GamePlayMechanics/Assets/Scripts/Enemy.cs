using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody enemyRb;
    private GameObject player;

    public float speed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //calculate vectore in the direction of the player with length (or magnitude or distance) = 1 (normalized)
        //in the direction of the player
        //making it normalized keeps the magintude = 1 (distance = 1) otherwise, the with more magnitude the force will increase accordingly below
        var lookDirection = (player.transform.position - transform.position).normalized; 

        enemyRb.AddForce(lookDirection * speed);

        //destroy enemies out of bounds
        if (transform.position.y < -10.0f)
        {
            Destroy(gameObject);
        }
    }
}

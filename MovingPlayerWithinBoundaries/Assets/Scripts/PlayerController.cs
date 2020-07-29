using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public List<GameObject> projectilePrefabs;

    //public GameObject projectilePrefab;
    public float maxSpeed = 10.0f;
    public float clampX = 10.0f;

    private float horizontalInput;

    void LateUpdate()
    {
        //If user presses the Spacebar button, launch a projectile from the player's position
        LaunchProjectile();

        //STEP-1: Move the player based on the horizontal input
        MovePlayer();

        //STEP-2: CLAMP to ensure the player moves within the boundary (-X to X)
        CheckBounds();
    }

    private void LaunchProjectile()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var randomProjectileIndex = Random.Range(0, projectilePrefabs.Count - 1);
            var projectile = projectilePrefabs[randomProjectileIndex];

            //launch a projectile from the player's position
            Instantiate(projectile, transform.position, projectile.transform.rotation);
        }
    }

    private void MovePlayer()
    {
        //STEP-1: MOVE
        //get horizontal input
        horizontalInput = Input.GetAxis("Horizontal");

        //move left or right
        transform.Translate(Vector3.right * horizontalInput * maxSpeed * Time.deltaTime);
    }

    private void CheckBounds()
    {
        //STEP-2: CLAMP to ensure the player moves within the boundary (-X to X)
        //Ensure player cannot move past to the clampX position
        if (transform.position.x >= clampX)
            transform.position = new Vector3(clampX, transform.position.y, transform.position.z);

        if (transform.position.x <= -clampX)
            transform.position = new Vector3(-clampX, transform.position.y, transform.position.z);
    }
}

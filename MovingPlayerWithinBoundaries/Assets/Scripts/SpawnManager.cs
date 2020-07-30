using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> animalPrefabs;
    public float spawnRangeX = 10;
    public float spawnPositionZ = 15;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            //randome animal index
            var randomAnimalIndex = Random.Range(0, animalPrefabs.Count - 1);
            var animal = animalPrefabs[randomAnimalIndex];

            //random X position within spawnRangeX
            var randPosX = Random.Range(-spawnRangeX, spawnRangeX);
            var spawnedAnimalPos = new Vector3(randPosX, 0, spawnPositionZ);

            //rand move forward speed (Note: The MoveForward component is applied to the animals)
            var moveForward = animal.GetComponent<MoveFotward>();
            moveForward.speed = Random.Range(3.75f, 10.0f);

            //launch a projectile from the player's position
            Instantiate(animal, spawnedAnimalPos, animal.transform.rotation);
        }
    }
}

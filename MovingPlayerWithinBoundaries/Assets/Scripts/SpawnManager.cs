using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Animals")]
    public List<GameObject> animalPrefabs;

    [Header("Spawn Settings")]
    public float spawnRangeX = 10;
    public float spawnPositionZ = 15;

    [Space]
    public float MinRepeatIntervalToSpawnAnimal = 1.5f;
    public float MaxRepeatIntervalToSpawnAnimal = 3.5f;

    // Start is called before the first frame update
    void Start()
    {
        var randomSeconds = Random.Range(MinRepeatIntervalToSpawnAnimal, MaxRepeatIntervalToSpawnAnimal); //random repeat rate in seconds

        InvokeRepeating(nameof(SpawnRandomAnimal), 0, randomSeconds);
    }

    // Update is called once per frame
    //Below code is commented because now we're spawning at randome intervals usng InvokeRepeating in Start 
    //void LateUpdate()
    //{
    //    if (Input.GetKeyDown(KeyCode.S))
    //    {
    //        SpawnRandomAnimal();
    //    }
    //}

    private void SpawnRandomAnimal()
    {
        //randome animal index
        var randomAnimalIndex = Random.Range(0, animalPrefabs.Count - 1);
        var animal = animalPrefabs[randomAnimalIndex];

        //random X position within spawnRangeX
        var randPosX = Random.Range(-spawnRangeX, spawnRangeX);
        var spawnedAnimalPos = new Vector3(randPosX, 0, spawnPositionZ);

        //rand move forward speed (Note: The MoveForward component is applied to the animals)
        var moveForward = animal.GetComponent<MoveFotward>();
        moveForward.speed = Random.Range(3.75f, 5.5f);

        //launch a projectile from the player's position
        Instantiate(animal, spawnedAnimalPos, animal.transform.rotation);
    }
}

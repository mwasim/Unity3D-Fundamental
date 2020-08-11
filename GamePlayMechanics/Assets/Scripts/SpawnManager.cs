using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> enemyPrefabList;
    public List<GameObject> powerUpPrefabList;
    public int enemyCount;
    public int enemiesPerWave = 1;

    private readonly float spawnRange = 9.0f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemiesWave(enemiesPerWave);

        InstantiatePowerUp();
    }

    private void SpawnEnemiesWave(int count)
    {
        //Instantiate X number of enemies and Powerups
        for (int i = 0; i < count; i++)
        {
            InstantiateEnemy();            
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;

        //if enemies are gone, spawn enemies again
        if(enemyCount < 1)
        {
            enemiesPerWave++; //increment number of enemies with each new wave
            SpawnEnemiesWave(enemiesPerWave);

            //instantiate new Powerup also,
            InstantiatePowerUp();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            InstantiateEnemy();
            InstantiatePowerUp();
        }
    }

    private void InstantiateEnemy()
    {
        //random position
        var randomPos = GenerateSpawnPosition();

        //random enemy prefab
        var randomEnemyPrefab = GenerateRandomEnemyPrefab();

        //instantiate a new enemy
        Instantiate(randomEnemyPrefab, randomPos, randomEnemyPrefab.transform.rotation);
    }

    private void InstantiatePowerUp()
    {
        //random position
        var randomPos = GenerateSpawnPosition(isPowerup: true);

        //random Powerup prefab
        var randomPowerupPrefab = GenerateRandomPowerupPrefab();

        //instantiate a new Powerup
        Instantiate(randomPowerupPrefab, randomPos, randomPowerupPrefab.transform.rotation);
    }

    private Vector3 GenerateSpawnPosition(bool isPowerup = false)
    {
        float multiplier = isPowerup ? 0.9f : 1.0f; //in case of power up, spawn position should be reduced a little bit
        var range = spawnRange * multiplier;

        float spawnPosX = Random.Range(-range, range);
        float spawnPosZ = Random.Range(-range, range);

        return new Vector3(spawnPosX, 0, spawnPosZ);
    }

    private GameObject GenerateRandomEnemyPrefab()
    {
        var index = Random.Range(0, enemyPrefabList.Count - 1);

        return enemyPrefabList[index];
    }

    private GameObject GenerateRandomPowerupPrefab()
    {
        var index = Random.Range(0, powerUpPrefabList.Count - 1);

        return powerUpPrefabList[index];
    }
}

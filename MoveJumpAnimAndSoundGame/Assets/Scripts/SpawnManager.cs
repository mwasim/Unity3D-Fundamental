using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> obstaclePrefabList;
    private Vector3 spawnPos = new Vector3(25, 0, 0);

    private readonly float startDelay = 3f;
    private readonly float repeatRate = 2.5f;

    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);

        //Find a game object by name
        playerController = GameObject.Find("Player").GetComponent<PlayerController>(); //script communication
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnObstacle()
    {
        if (!playerController.isGameOver)
        {
            var randomIndex = Random.Range(0, obstaclePrefabList.Count - 1);
            var obstaclePrefab = obstaclePrefabList[randomIndex];

            Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
        }
    }
}

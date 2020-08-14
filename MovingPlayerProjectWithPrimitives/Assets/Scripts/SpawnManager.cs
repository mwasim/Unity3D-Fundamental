using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject workerPrefab;
    public List<GameObject> spawnPointList;

    private float minWaitTime = 1.0f;
    private float maxWaitTime = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(nameof(SpawnTimer));
    }
   
    IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));

        InstantiateRandomWorker();
    }

    private void InstantiateRandomWorker()
    {
        var index = Random.Range(0, spawnPointList.Count - 1);
        var spawnPoint = spawnPointList[index];

        Instantiate(workerPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);


        StartCoroutine(nameof(SpawnTimer)); //call the coroutine again
    }
}

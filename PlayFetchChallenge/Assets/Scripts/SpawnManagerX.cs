using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject[] ballPrefabs;

    private readonly float spawnLimitXLeft = -22;
    private readonly float spawnLimitXRight = 7;
    private readonly float spawnPosY = 30;

    private readonly float startDelay = 1.0f;

    private readonly float spwanMinInterval = 3.0f;
    private readonly float spawnMaxInterval = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnRandomBall), startDelay, Random.Range(spwanMinInterval, spawnMaxInterval));
    }

    // Spawn random ball at random x position at top of play area
    void SpawnRandomBall()
    {
        // Generate random ball index and random spawn position
        Vector3 spawnPos = new Vector3(Random.Range(spawnLimitXLeft, spawnLimitXRight), spawnPosY, 0);

        var randomBallIndex = Random.Range(0, ballPrefabs.Length - 1);

        // instantiate ball at random spawn location
        Instantiate(ballPrefabs[randomBallIndex], spawnPos, ballPrefabs[randomBallIndex].transform.rotation);
    }

}

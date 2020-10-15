using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] CoinController coinPrefab;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 300; i++)
        {
            Vector2 spawnPosition =
                   Camera.main.ViewportToWorldPoint(new Vector2(Random.value, Random.value));

            Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
        }
    }
}

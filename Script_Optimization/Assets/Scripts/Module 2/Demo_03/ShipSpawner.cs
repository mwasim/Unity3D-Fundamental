using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSpawner : MonoBehaviour
{
    [SerializeField] private ShipController shipPrefab;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 500; i++)
        {
            Vector2 spawnPosition =
                   Camera.main.ViewportToWorldPoint(new Vector2(Random.value, Random.value));

            Instantiate(shipPrefab, spawnPosition, Quaternion.identity);

        }
    }
}


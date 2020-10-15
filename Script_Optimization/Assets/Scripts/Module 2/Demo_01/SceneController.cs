using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    private float enemyTimer;

    private void Start()
    {
        enemyTimer = Time.unscaledTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.unscaledTime > enemyTimer + .05f)
        {
            enemyTimer = Time.unscaledTime;

            Vector2 spawnPosition =
                Camera.main.ViewportToWorldPoint(new Vector2(Random.value, 1));

            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        }
    }
}

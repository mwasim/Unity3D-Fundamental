﻿using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Singleton (Other classes can access this single instance)
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("GameManager is NULL");
            }

            return _instance;
        }
    }

    //To intialize the Singleton we use the Awake() method
    private void Awake()
    {
        _instance = this;
    }

    private static float _timeElapsed = 0;

    public void DisplayGameTimeElapsed()
    {
        _timeElapsed += Time.time;
        Debug.Log($"Game time elapsed: {_timeElapsed: #0.00}");
    }
}

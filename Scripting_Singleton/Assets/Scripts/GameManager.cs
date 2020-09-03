using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void DisplayGameTimeElapsed()
    {
        Debug.Log("Game time elapsed: 45 seconds");
    }
}

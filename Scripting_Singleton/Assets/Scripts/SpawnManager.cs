using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //Singleton (Other classes can access this single instance)
    private static SpawnManager _instance;
    public static SpawnManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("SpawnManager is NULL");
            }

            return _instance;
        }
    }

    //To intialize the Singleton we use the Awake() method
    private void Awake()
    {
        _instance = this;
    }

    public void SpawnItem()
    {
        Debug.Log("Item is spawned");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefabObject;
    [SerializeField] private int poolDepth; // number of objects to be stored in the pool
    [SerializeField] private bool canGrow = true;

    private readonly List<GameObject> pool = new List<GameObject>();

    private void Awake() //it's better to populate the pool in the Awake function
    {
        for (int i = 0; i < poolDepth; i++)
        {
            GameObject pooledObject = Instantiate(prefabObject);
            pooledObject.SetActive(false); //by default inactive
            pool.Add(pooledObject);
        }
    }

    public GameObject GetAvailableObject()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (pool[i].activeInHierarchy == false)
                return pool[i];
        }

        if (canGrow == true)
        {
            GameObject pooledObject = Instantiate(prefabObject); //This are still objects instantiated during the game play so there's is cost involved, so it's better to set the pool size so that there is no need to instantiate objects during the gameplay
            pooledObject.SetActive(false);
            pool.Add(pooledObject);

            return pooledObject;
        }
        else
            return null;
    }
}

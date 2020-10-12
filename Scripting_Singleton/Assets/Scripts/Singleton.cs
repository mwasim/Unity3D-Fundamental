using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T> //Requires the type (T) is an object derived from Singleton<T>, for example T cannot be any GameObject
{
    private static T instance;

    public static T Instance => instance; //expose the getter only


    //Check if the instance is already initialized?
    public static bool IsInitialized => instance != null;

    //Make if overridable in the derived classes
    protected virtual void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("[Singleton] Trying to instantiate a second instance of a Singleton class");
        }
        else
        {
            instance = (T)this; //ensure that "this" object is of type T
        }
    }

    protected virtual void OnDestroy()
    {
        //if this object is destroyed another instance should be created
        if (instance == this)
        {
            instance = null;
        }
    }
}

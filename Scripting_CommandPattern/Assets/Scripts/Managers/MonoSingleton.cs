using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    //Singleton (Other classes can access this single instance)
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError($"{typeof(T)} is NULL");
            }

            return _instance;
        }
    }

    //To intialize the Singleton we use the Awake() method
    private void Awake()
    {
        _instance = this as T; //Or can also be casteed as (T)this;

        Init();
    }

    //abstract methods cannot have the method body while virtual methods must have the method body
    /// <summary>
    /// Init is fired in Awake
    /// </summary>
    public virtual void Init()
    {
        //Option - the derived classes may override this method
    }
}

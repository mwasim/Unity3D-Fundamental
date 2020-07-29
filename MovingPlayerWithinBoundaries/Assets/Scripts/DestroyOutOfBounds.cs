﻿using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float topBound = 30f;
    private float lowerBound = -10f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Normally objects move in the Z direction, so when the object is gone out of bounds, destroy it
        var shouldDestroy = transform.position.z > topBound || transform.position.z < lowerBound;
        if (shouldDestroy)
        {
            Destroy(gameObject); //gameObject is the object this script is applied to
        }
    }
}

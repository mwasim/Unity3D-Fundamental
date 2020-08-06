using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPosition;

    private float repeatWidth;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;

        repeatWidth = GetComponent<BoxCollider>().size.x / 2; //half of the width of the background (as collider is applied on the background)
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < startPosition.x - repeatWidth)
        {
            transform.position = startPosition;
        }
    }
}

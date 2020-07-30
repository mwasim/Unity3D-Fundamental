using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject); //Destory the object this script is applied to
        Destroy(other.gameObject); //Destroy the object the collision is detected with
    }
}

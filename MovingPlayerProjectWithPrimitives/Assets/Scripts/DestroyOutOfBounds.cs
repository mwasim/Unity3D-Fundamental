using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float topBound = 20f;
   
    // Update is called once per frame
    void LateUpdate()
    {
        //Normally objects move in the Z direction, so when the object is gone out of bounds, destroy it
        var shouldDestroy = transform.position.z > topBound;
        if (shouldDestroy)
        {
            Destroy(gameObject); //gameObject is the object this script is applied to
        }
    }
}

using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    // Update is called once per frame
    void FixedUpdate()
    {
        //LookDirectionToFace = destination - source

        var directionToFace = target.position - transform.position;

        //draw a ray
        Debug.DrawRay(transform.position, directionToFace, Color.green);


        //Rotate to look at the target
        transform.rotation = Quaternion.LookRotation(directionToFace);
    }
}

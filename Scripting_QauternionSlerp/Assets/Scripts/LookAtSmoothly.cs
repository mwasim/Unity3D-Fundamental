using UnityEngine;

public class LookAtSmoothly : MonoBehaviour
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
        var targetRotation = Quaternion.LookRotation(directionToFace);

        //Slerp makes the rotation very smooth (instead of snapping to the target rotation)
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
    }
}

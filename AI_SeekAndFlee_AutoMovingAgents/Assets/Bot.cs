using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : MonoBehaviour
{
    NavMeshAgent agent;

    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Seek(Vector3 destinationLocation)
    {
        //In this case, the destinationLocation is going to be the COP
        agent.SetDestination(destinationLocation);
    }

    private void Flee(Vector3 location)
    {
        /*
         * location => cop's position, transform.position => robber's position
         * We can set the flee position in the opposite direction to the cop
        */
        var fleeVector = location - transform.position; //gives vector in the direction of the location
        var destination = transform.position - fleeVector; //to make it opposite with same magnitude, simply subtract from the current position

        agent.SetDestination(destination);

    }

    /*
        Pursuit is similar to Seek. The difference is, the Persuer predicts the future location of the target and intercepts
     */
    private void Pursue()
    {
        //Predict future location
        var targetCurrentSpeed = target.GetComponent<Drive>().currentSpeed;

        var targetDirection = target.transform.position - transform.position;
        /*
            For relative angle, translate the target's forward dir relative to the space of the agent
         */
        var relativeHeading = Vector3.Angle(transform.forward, transform.TransformVector(target.transform.forward));
        var toTarget = Vector3.Angle(transform.forward, transform.TransformVector(targetDirection));

        //SCENARIO-1: If the target has stopped moving, simply seek and return
        //SCENARIO-2: OR Check if the angle is small (relativeHeading)
        if ((toTarget > 90 && relativeHeading < 20) || targetCurrentSpeed < 0.01f) //we're not comparing with ZERO because sometimes there's is floating point error
        {
            Seek(target.transform.position);
            Debug.Log("JUST SEEKING...");
            return;
        }

        var lookAhead = targetDirection.magnitude / (agent.speed + targetCurrentSpeed);

        //Determin seek location, and seek
        //target.transform.forward has magnitude = 1 (already normalized) -> forward is the direction in which the object is moving
        var seekLocation = target.transform.position + target.transform.forward * lookAhead;
        Seek(seekLocation);
        Debug.Log("PURSUING...");
    }

    // Update is called once per frame
    void Update()
    {
        //Testing Seek
        //Seek(target.transform.position);

        //Testing Flee
        //Flee(target.transform.position);

        //Testing Pursuite
        Pursue();
    }
}

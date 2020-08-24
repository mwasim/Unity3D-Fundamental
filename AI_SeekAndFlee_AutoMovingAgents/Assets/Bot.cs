﻿using UnityEngine;
using UnityEngine.AI;

public class Bot : MonoBehaviour
{
    private NavMeshAgent agent;
    private Drive driveScript;

    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        driveScript = target.GetComponent<Drive>();
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
        var targetCurrentSpeed = driveScript.currentSpeed;

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

    /*
        Evade is similar to Pursue. Except it uses the Flee instead of Seek (Opposite of Pursue).
     */
    private void Evade()
    {
        var targetCurrentSpeed = driveScript.currentSpeed;
        var targetDirection = target.transform.position - transform.position;        

        var lookAhead = targetDirection.magnitude / (agent.speed + targetCurrentSpeed);

        //Determin flee location, and seek
        //target.transform.forward has magnitude = 1 (already normalized) -> forward is the direction in which the object is moving
        var fleeLocation = target.transform.position + target.transform.forward * lookAhead;

        Flee(fleeLocation);
    }

    Vector3 wanderTarget = Vector3.zero;
    private void Wander()
    {
        //These variables can be adjusted to adjust Wander behavior
        //Ensure the position you're calculating exists on the NavMesh, otherwise it may cause location issues
        var wanderRadius = 10.0f; //adjust the wander circle
        var wanderDistance = 1.0f;
        var wanderJitter = 1.0f;

        var wanderAxisValue = Random.Range(-1.0f, 1.0f) * wanderJitter;
        wanderTarget += new Vector3(wanderAxisValue,
            0f,
            wanderAxisValue);

        wanderTarget.Normalize(); //sets magnitude to 1
        wanderTarget *= wanderRadius;

        var targetLocal = wanderTarget + new Vector3(0, 0, wanderDistance);
        var targetWorld = this.gameObject.transform.InverseTransformVector(targetLocal);

        Seek(targetWorld);
    }

    // Update is called once per frame
    void Update()
    {
        //Testing Seek
        //Seek(target.transform.position);

        //Testing Flee
        //Flee(target.transform.position);

        //Testing Pursuite
        //Pursue();

        //Testing Evade
        //Evade();

        //Testing Wander
        Wander();
    }
}

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

    // Update is called once per frame
    void Update()
    {
        //Testing Seek
        //Seek(target.transform.position);

        //Testing Flee
        Flee(target.transform.position);
    }
}

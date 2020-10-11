using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public float patrolTime = 15; // time in seconds to wait before seeking a new patrol destination
    public float aggroRange = 10; // distance in scene units below which the NPC will increase speed and seek the player
    public Transform[] waypoints; // collection of waypoints which define a patrol area

    int index; // the current waypoint index in the waypoints array
    float speed, agentSpeed; // current agent speed and NavMeshAgent component speed
    Transform player; // reference to the player object transform

    Animator animator; // reference to the animator component
    NavMeshAgent agent; // reference to the NavMeshAgent

    void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        if (agent != null) { agentSpeed = agent.speed; }
        player = GameObject.FindGameObjectWithTag("Player").transform;
        index = Random.Range(0, waypoints.Length); //use randome waypoint, index is changed every (patrolTime) seconds

        //Timers
        InvokeRepeating(nameof(Tick), 0, 0.5f); //repeat the Tick method every half second


        //Petrol on checking waypoints to make it more robust
        if(waypoints.Length > 0)
        {
            InvokeRepeating(nameof(Patrol), 0, patrolTime); //repeat the Tick method every half second
        }
    }

    private void Patrol() //manages the waypoint index
    {
        index = index == waypoints.Length - 1 ? 0 : index + 1; //reached to the end of the array or not
    }

    private void Tick()
    {
        //assuming the default behavior of NPC is patroling
        agent.destination = waypoints[index].position;
    }
}

using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public float patrolTime = 10;
    public float aggroRange = 10;
    public Transform[] waypoints;
    public AttackDefinition attack;

    public Transform SpellHotSpot; //position to spawn spells

    int index;
    float speed, agentSpeed;
    Transform player;

    Animator animator;
    NavMeshAgent agent;

    private float timeOfLastAttack;
    private bool playerIsAlive;

    void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agentSpeed = agent.speed;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        index = Random.Range(0, waypoints.Length);

        InvokeRepeating("Tick", 0, 0.5f);

        if (waypoints.Length > 0)
        {
            InvokeRepeating("Patrol", Random.Range(0, patrolTime), patrolTime);
        }

        timeOfLastAttack = float.MinValue;
        playerIsAlive = true;
    }

    private void PlayerDied()
    {
        playerIsAlive = false;
    }

    void Update()
    {
        speed = Mathf.Lerp(speed, agent.velocity.magnitude, Time.deltaTime * 10);
        animator.SetFloat("Speed", speed);

        float timeSinceLastAttack = Time.time - timeOfLastAttack;
        Debug.Log("Attack: " + attack);
        bool attackOnCooldown = timeSinceLastAttack < attack?.CoolDown;

        agent.isStopped = attackOnCooldown;

        if (playerIsAlive)
        {
            float distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
            bool attackInRange = distanceFromPlayer < attack?.Range;

            if (!attackOnCooldown && attackInRange)
            {
                transform.LookAt(player.transform);
                timeOfLastAttack = Time.time;
                animator.SetTrigger("Attack");
            }
        }
    }

    public void Hit()
    {
        if (!playerIsAlive)
            return;

        if (attack is WeaponDefinition)
        {
            ((WeaponDefinition)attack).ExecuteAttack(gameObject, player.gameObject);
        }
        else if (attack is Spell)
        {
            Debug.Log(SpellHotSpot);
            ((Spell)attack).Cast(gameObject, SpellHotSpot.position, player.transform.position, LayerMask.NameToLayer("EnemySpells"));
            //GetComponent<AudioSource>().PlayOneShot(spellClip);  //todo 
        }
    }

    void Patrol()
    {
        index = index == waypoints.Length - 1 ? 0 : index + 1;
    }

    void Tick()
    {
        agent.destination = waypoints[index].position;
        agent.speed = agentSpeed / 2;

        if (player != null && Vector3.Distance(transform.position, player.transform.position) < aggroRange)
        {
            agent.speed = agentSpeed;
            agent.destination = player.position;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }
}

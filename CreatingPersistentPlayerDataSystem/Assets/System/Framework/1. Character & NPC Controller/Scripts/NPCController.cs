using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{

    public float patrolTime = 10;
    public float aggroRange = 10;
    public Transform[] waypoints;
    public AttackDefinition attack;
    public Transform hotSpot;

    int index;
    float speed, agentSpeed;

    Transform player;
    Animator animator;
    NavMeshAgent agent;

    private float lastAttackTime;

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

        lastAttackTime = float.MinValue;
    }

    void Update()
    {
        speed = Mathf.Lerp(speed, agent.velocity.magnitude, Time.deltaTime * 10);
        animator.SetFloat("Speed", speed);

        float timeSinceLastAttack = Time.time - lastAttackTime;
        bool attackonCooldown = timeSinceLastAttack < attack.Cooldown;

        agent.isStopped = attackonCooldown;

        if (player != null)
        {
            bool attackInRange = Vector3.Distance(transform.position, player.transform.position) < attack.range;

            //If attack is not on cooldown and we are close enough... attack
            if (!attackonCooldown && attackInRange)
            {
                transform.LookAt(player.transform);
                lastAttackTime = Time.time;
                animator.SetTrigger("Attack");

            }
        }
    }

    void Patrol()
    {
        index = index == waypoints.Length - 1 ? 0 : index + 1;
    }

    public void Stop()
    {
        if(agent != null)
            agent.isStopped = true;
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

    public void Hit()
    {
        if (player != null)
        {
            if (attack is Weapon)
            {
                ((Weapon) attack).ExecuteAttack(gameObject, player.gameObject, 1);
            }
            else if (attack is Spell)
            {
                ((Spell) attack).Cast(gameObject, hotSpot.position, player, LayerMask.NameToLayer("EnemySpell"));
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }

}


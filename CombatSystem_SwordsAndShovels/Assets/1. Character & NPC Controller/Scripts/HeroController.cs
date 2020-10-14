using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class HeroController : MonoBehaviour
{
    public AttackDefinition demoAttack;
    Animator animator;
    NavMeshAgent agent;
    CharacterStats stats;

    private GameObject _attackTarget;

    void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        stats = GetComponent<CharacterStats>();
    }

    void Update()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    public void SetDestination(Vector3 destination)
    {
        //on set destination, we should stop all coroutines
        StopAllCoroutines();
        agent.isStopped = false; //just in case, we recently stopped the agent

        agent.destination = destination;
    }

    public void AttackTarget(GameObject target)
    {
        /*
         //demo attack code is commented now
        var attack = demoAttack.CreateAttack(stats, target.GetComponent<CharacterStats>());

        var attackables = target.GetComponentsInChildren<IAttackable>();

        foreach (var attackable in attackables)
        {
            attackable.OnAttack(gameObject, attack);
        }*/


        var weapon = stats.GetCurrentWeapon();

        if(weapon != null)
        {
            StopAllCoroutines(); //stop pususing the target

            agent.isStopped = false; //ensure the agent is not stopped

            _attackTarget = target; //set to passed in game object  - target

            StartCoroutine(PursueAndAttackTarget()); //start coroutine
        }
    }

    //to trigger the attack, we want our hero to approach and attack
    private IEnumerator PursueAndAttackTarget()
    {
        agent.isStopped = false;
        var weapon = stats.GetCurrentWeapon();

        //pursue the target until it's within the range
        while(Vector3.Distance(transform.position, _attackTarget.transform.position) > weapon.Range) //keep the hero moving towards the target until it's within the range
        {
            agent.destination = _attackTarget.transform.position;

            yield return null;
        }

        //as the target is now in the range, the hero should stop
        agent.isStopped = true;

        //ensure the hero is facing the target
        transform.LookAt(_attackTarget.transform);

        //animate
        animator.SetTrigger("Attack");
    }

    //Hit is an event on the Attack Animation of the Hero
    public void Hit()
    {
        //Have our weapon attack the target
        if (_attackTarget != null)
        {
            stats.GetCurrentWeapon().ExecuteAttack(gameObject, _attackTarget);
        }
    }
}

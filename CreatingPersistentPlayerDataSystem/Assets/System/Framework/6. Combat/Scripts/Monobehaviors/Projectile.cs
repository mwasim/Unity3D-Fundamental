using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Spell spellDefinition;
    private GameObject caster;
    private Vector3 direction;

    public void Fire(Spell spellDefinition, GameObject caster, Transform target)
    {
        this.spellDefinition = spellDefinition;
        this.caster = caster;

        direction = target.position - transform.position;
        direction.y = 0.0f;
        direction.Normalize();
        direction *= spellDefinition.ProjectileSpeed;

        Destroy(gameObject, 5);
    }

    private void Update()
    {
        transform.Translate(direction * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        var go = other.gameObject;

        var attack =
            spellDefinition.CreateAttack(caster.GetComponent<CharacterStats>(), go.GetComponent<CharacterStats>(), 1);

        var attackables = go.GetComponentsInChildren(typeof(IAttackable));

        foreach (var a in attackables)
        {
            ((IAttackable) a).OnAttack(caster.gameObject, attack);
        }

        Destroy(gameObject);
    }
}
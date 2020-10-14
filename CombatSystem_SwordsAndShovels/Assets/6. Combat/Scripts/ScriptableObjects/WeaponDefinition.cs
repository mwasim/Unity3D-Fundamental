using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon.asset", menuName = "Attack/Weapon")]
public class WeaponDefinition : AttackDefinition
{
    public Rigidbody weaponPrefab;

    public void ExecuteAttack(GameObject attacker, GameObject defender)
    {
        if (defender == null) return;

        //check defender is within the range of the attacker
        if (Vector3.Distance(attacker.transform.position, defender.transform.position) > Range) return;

        //Check if the defender is infront of the player
        if (!attacker.transform.IsFacingTarget(defender.transform)) return;

        //at this point the attack happens
        var attackerStats = attacker.GetComponent<CharacterStats>();
        var defenderStats = attacker.GetComponent<CharacterStats>();

        var attack = CreateAttack(attackerStats, defenderStats);

        //iterate over all of the behaviors implementing the IAttackable
        var attackables = defender.GetComponentsInChildren<IAttackable>();

        foreach (var attackable in attackables)
        {
            //call the OnAttack method
            attackable.OnAttack(attacker, attack);
        }
    }
}

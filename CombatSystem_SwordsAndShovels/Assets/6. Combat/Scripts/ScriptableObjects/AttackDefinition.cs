using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack.asset", menuName = "Attack/Base Attack")]
public class AttackDefinition : ScriptableObject
{
    public float CoolDown;
    public float Range;
    public float MinDamage;
    public float MaxDamage;
    public float CriticalMultiplier;
    public float CriticalChance;


    public Attack CreateAttack(CharacterStats wielderStats, CharacterStats defenderStats)
    {
        float coreDamage = wielderStats.GetDamage();
        coreDamage += Random.Range(MinDamage, MaxDamage); //constitutes our dice roll

        bool isCritical = Random.value < CriticalChance;
        if (isCritical)
            coreDamage *= CriticalMultiplier;

        if (defenderStats != null)
            coreDamage -= defenderStats.GetDamage();

        return new Attack((int)coreDamage, isCritical);
    }
}

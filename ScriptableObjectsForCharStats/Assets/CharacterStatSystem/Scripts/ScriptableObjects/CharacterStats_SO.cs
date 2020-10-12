using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStats", menuName = "Character/Stats", order = 1)]
public class CharacterStats_SO : ScriptableObject
{

    [System.Serializable] //Unity will serialize this class and display it in the Unity's inspector
    public class CharLevelUps
    {
        public int maxHealth;
        public int maxMana;
        public int maxWealth;
        public int baseDamage;
        public int baseResistance;
        public int maxEncumbrance;
    }

    //FIELDS
    public bool setManually = false;
    public bool saveDataOnClose = false;

    public ItemPickUp Weapon { get; private set; }
    public ItemPickUp HeadArmor { get; private set; }
    public ItemPickUp ChestArmor { get; private set; }
    public ItemPickUp HandArmor { get; private set; }
    public ItemPickUp LegArmor { get; private set; }
    public ItemPickUp FootArmor { get; private set; }
    public ItemPickUp Misc1 { get; private set; }
    public ItemPickUp Misc2 { get; private set; }

    public int maxHealth = 0;
    public int currentHealth = 0;

    public int maxMana = 0;
    public int currentMana = 0;

    public int maxWealth = 0;
    public int currentWealth = 0;

    public int baseDamage = 0;
    public int currentDamage = 0;

    public float baseResistance = 0f;
    public float currentResistance = 0f;

    public float currentEncumbrance = 0f;

    public int charExperience = 0;
    public int charLevel = 0;

    public CharLevelUps[] charLevelUps;


    //STAT INCREASERS
    public void ApplyHealth(int healthAmount)
    {
        if (currentHealth + healthAmount > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += healthAmount;
        }
    }

    public void ApplyMana(int manaAmount)
    {
        if (currentMana + manaAmount > maxMana)
        {
            currentMana = maxMana;
        }
        else
        {
            currentMana += manaAmount;
        }
    }

    public void ApplyWealth(int wealthAmount)
    {
        if (currentWealth + wealthAmount > maxWealth)
        {
            currentWealth = maxWealth;
        }
        else
        {
            currentWealth += wealthAmount;
        }
    }

    public void ApplyDamage(int damageAmount)
    {
        if (currentDamage + damageAmount > baseDamage)
        {
            currentDamage = baseDamage;
        }
        else
        {
            currentDamage += damageAmount;
        }
    }

    public void ApplyResistance(int resistanceAmount)
    {
        if (currentResistance + resistanceAmount > baseResistance)
        {
            currentResistance = baseResistance;
        }
        else
        {
            currentResistance += resistanceAmount;
        }
    }


    //STAT DECREASERS
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            //chracter dies
            Death(); 
        }
    }

    public void TakeMana(int amount)
    {
        currentMana -= amount;

        if (currentMana < 0)
        {
            currentMana = 0; //mana cannot go below ZERO
        }
    }

    public void TakeWealth(int amount)
    {
        currentWealth -= amount;

        if (currentWealth < 0)
        {
            currentWealth = 0; //Wealth cannot go below ZERO
        }
    }

    public void TakeResistance(int amount)
    {
        currentResistance -= amount;

        if (currentResistance < 0)
        {
            currentResistance = 0; //Resistance cannot go below ZERO
        }
    }


    //CHARACTER LEVEL UP AND DEATH
    private void Death()
    {
        Debug.Log("Character is dead");

        //TODO: 1- Call the GameManager for Death Stat to Trigger Respawn
        //TODO: 2- Display Death Visualisations
    }
}

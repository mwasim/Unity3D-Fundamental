using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    public CharacterStats_SO characterDefinitionTemplate;
    public CharacterStats_SO characterDefinition;

    public CharacterInventory charInv;
    public GameObject characterWeaponSlot;
    public ItemPickUp defaultWeapon;

    public bool regenHealth = true;

    #region Constructors
    public CharacterStats()
    {
        charInv = CharacterInventory.instance;
    }
    #endregion

    #region Initializations
    void Start()
    {
        if (null != characterDefinitionTemplate)
            characterDefinition = Instantiate(characterDefinitionTemplate);

        if (!characterDefinition.setManually)
        {
            characterDefinition.maxHealth = 100;
            characterDefinition.CurrentHealth = 50;

            characterDefinition.maxMana = 25;
            characterDefinition.currentMana = 10;

            characterDefinition.maxWealth = 500;
            characterDefinition.currentWealth = 0;

            characterDefinition.baseDamage = 2;
            characterDefinition.currentDamage = characterDefinition.baseDamage;

            characterDefinition.baseResistance = 0;
            characterDefinition.currentResistance = 0;

            characterDefinition.maxEncumbrance = 50f;
            characterDefinition.currentEncumbrance = 0f;

            characterDefinition.charExperience = 0;
            //characterDefinition.charLevel = 1;
        }

        if (defaultWeapon != null)
        {
            characterDefinition.EquipWeapon(defaultWeapon, characterWeaponSlot);
        }

        characterDefinition.Init();

        InvokeRepeating("RegenHealth", 1, 1);
    }
    #endregion

    #region Stat Increasers
    public void ApplyHealth(int healthAmount)
    {
        characterDefinition.ApplyHealth(healthAmount);
    }

    public void ApplyMana(int manaAmount)
    {
        characterDefinition.ApplyMana(manaAmount);
    }

    public void GiveWealth(int wealthAmount)
    {
        characterDefinition.GiveWealth(wealthAmount);
    }

    void RegenHealth()
    {
        ApplyHealth(5);
    }
    #endregion

    #region Stat Reducers
    public void TakeDamage(int amount)
    {
        characterDefinition.TakeDamage(amount);
    }

    public void TakeMana(int amount)
    {
        characterDefinition.TakeMana(amount);
    }
    #endregion

    #region Weapon and Armor Change
    public void ChangeWeapon(ItemPickUp weaponPickUp)
    {
        if (!characterDefinition.UnEquipWeapon(weaponPickUp, charInv, characterWeaponSlot))
        {
            characterDefinition.EquipWeapon(weaponPickUp, charInv, characterWeaponSlot);
        }
    }

    public void ChangeArmor(ItemPickUp armorPickUp)
    {
        if (!characterDefinition.UnEquipArmor(armorPickUp, charInv))
        {
            characterDefinition.EquipArmor(armorPickUp, charInv);
        }
    }
    #endregion

    #region Reporters
    public int GetHealth()
    {
        return characterDefinition.CurrentHealth;
    }

    public Weapon GetCurrentWeapon()
    {
        return characterDefinition.Weapon != null ? characterDefinition.Weapon.itemDefinition.weaponSlotObject : null;
    }

    public void ExecuteAttack(GameObject attacker, GameObject target)
    {
        // aggregate attack hitpoints here
        characterDefinition.AggregateAttackPoints(GetCurrentWeapon().ExecuteAttack(attacker, target, characterDefinition.LevelMultiplier));
    }

    public int GetDamage()
    {
        return characterDefinition.currentDamage;
    }

    public float GetResistance()
    {
        return characterDefinition.currentResistance;
    }

    #endregion
}
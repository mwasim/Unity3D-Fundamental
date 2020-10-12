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

    public float maxEncumbrance = 0f;
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

    public void GiveWealth(int wealthAmount)
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

    //public void ApplyDamage(int damageAmount)
    //{
    //    if (currentDamage + damageAmount > baseDamage)
    //    {
    //        currentDamage = baseDamage;
    //    }
    //    else
    //    {
    //        currentDamage += damageAmount;
    //    }
    //}

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

    //EQUIP WEAPONS
    public void EquipWeapon(ItemPickUp weaponPickUp, CharacterInventory characterInventory, GameObject weaponSlot)
    {
        Weapon = weaponPickUp;

        currentDamage = baseDamage + weaponPickUp.itemDefinition.itemAmount;
    }

    //UN-EQUIP WEAPON
    public bool UnEquipWeapon(ItemPickUp weaponPickup, CharacterInventory characterInventory, GameObject weaponSlot)
    {
        bool prevWeaponSame = false;

        if (Weapon != null)
        {
            if (Weapon == weaponPickup)
                prevWeaponSame = true;

            Destroy(weaponSlot.transform.GetChild(0).gameObject); //destroy the first child of the weapon slot game object/transform

            //reset
            Weapon = null;
            currentDamage = baseDamage;
        }

        return prevWeaponSame;
    }

    //EQUIP ARMOR
    public void EquipArmor(ItemPickUp armorPickup, CharacterInventory characterInventory)
    {
        switch (armorPickup.itemDefinition.itemArmorSubType)
        {
            case ItemArmorSubtype.HEAD:
                HeadArmor = armorPickup;
                currentResistance += armorPickup.itemDefinition.itemAmount;
                break;
            case ItemArmorSubtype.CHEST:
                ChestArmor = armorPickup;
                currentResistance += armorPickup.itemDefinition.itemAmount;
                break;
            case ItemArmorSubtype.HANDS:
                HandArmor = armorPickup;
                currentResistance += armorPickup.itemDefinition.itemAmount;
                break;
            case ItemArmorSubtype.LEGS:
                LegArmor = armorPickup;
                currentResistance += armorPickup.itemDefinition.itemAmount;
                break;
            case ItemArmorSubtype.BOOTS:
                FootArmor = armorPickup;
                currentResistance += armorPickup.itemDefinition.itemAmount;
                break;
        }
    }

    //UN-EQUIP ARMOR
    public bool UnEquipArmor(ItemPickUp armorPickup, CharacterInventory characterInventory)
    {
        bool prevArmorSame = false;

        switch (armorPickup.itemDefinition.itemArmorSubType)
        {
            case ItemArmorSubtype.HEAD:
                if (HeadArmor != null)
                {
                    if (HeadArmor == armorPickup)
                        prevArmorSame = true;

                    //unequip
                    currentResistance -= armorPickup.itemDefinition.itemAmount;
                    //reset
                    HeadArmor = null;
                }
                break;
            case ItemArmorSubtype.CHEST:
                if (ChestArmor != null)
                {
                    if (ChestArmor == armorPickup)
                        prevArmorSame = true;

                    //unequip
                    currentResistance -= armorPickup.itemDefinition.itemAmount;
                    //reset
                    ChestArmor = null;
                }
                break;
            case ItemArmorSubtype.HANDS:
                if (HandArmor != null)
                {
                    if (HandArmor == armorPickup)
                        prevArmorSame = true;

                    //unequip
                    currentResistance -= armorPickup.itemDefinition.itemAmount;
                    //reset
                    HandArmor = null;
                }
                break;
            case ItemArmorSubtype.LEGS:
                if (LegArmor != null)
                {
                    if (LegArmor == armorPickup)
                        prevArmorSame = true;

                    //unequip
                    currentResistance -= armorPickup.itemDefinition.itemAmount;
                    //reset
                    LegArmor = null;
                }
                break;
            case ItemArmorSubtype.BOOTS:
                if (FootArmor != null)
                {
                    if (FootArmor == armorPickup)
                        prevArmorSame = true;

                    //unequip
                    currentResistance -= armorPickup.itemDefinition.itemAmount;
                    //reset
                    FootArmor = null;
                }
                break;
        }

        return prevArmorSame;
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

    private void LevelUp()
    {
        charLevel += 1;

        //TODO: DISPLAY LEVEL UP VISUALISATIONS

        //TODO: INCREASE LEVEL STATS BASED ON THE LEVEL

        //increase stats
        maxHealth = charLevelUps[charLevel - 1].maxHealth;
        maxMana = charLevelUps[charLevel - 1].maxMana;
        maxWealth = charLevelUps[charLevel - 1].maxWealth;
        baseDamage = charLevelUps[charLevel - 1].baseDamage;
        baseResistance = charLevelUps[charLevel - 1].baseResistance;
        maxEncumbrance = charLevelUps[charLevel - 1].maxEncumbrance;
    }
}

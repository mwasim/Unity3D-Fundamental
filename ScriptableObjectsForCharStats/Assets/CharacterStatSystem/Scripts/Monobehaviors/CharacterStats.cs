using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    [SerializeField] private CharacterStats_SO _characterDefinition;
    [SerializeField] private CharacterInventory _characterInventory;
    [SerializeField] private GameObject _characterWeaponSlot;

    //CONSTRUCTORS
    public CharacterStats()
    {
        _characterInventory = CharacterInventory.Instance;
    }


    //INITIALIZATIONS
    private void Start()
    {
        if (!_characterDefinition.setManually)
        {
            _characterDefinition.maxHealth = 100;
            _characterDefinition.currentHealth = 50;

            _characterDefinition.maxMana = 25;
            _characterDefinition.currentMana = 10;

            _characterDefinition.maxWealth = 500;
            _characterDefinition.currentWealth = 0;

            _characterDefinition.baseResistance = 0;
            _characterDefinition.currentResistance = 0;

            _characterDefinition.maxEncumbrance = 50f;
            _characterDefinition.currentResistance = 0f;

            _characterDefinition.charExperience = 0;
            _characterDefinition.charLevel = 1;
        }
    }

    //STATS INCREASERS
    public void ApplyHealth(int amount)
    {
        _characterDefinition.ApplyHealth(amount);
    }

    public void ApplyMana(int amount)
    {
        _characterDefinition.ApplyMana(amount);
    }

    public void GiveWealth(int amount)
    {
        _characterDefinition.GiveWealth(amount);
    }

    //STATS REDUCERS
    public void TakeDamage(int amount)
    {
        _characterDefinition.TakeDamage(amount);
    }

    public void TakeMana(int amount)
    {
        _characterDefinition.TakeMana(amount);
    }


    //WEAPON AND ARMOR CHANGE
    public void ChangeWeapon(ItemPickUp weaponPickup)
    {
        if (!_characterDefinition.UnEquipWeapon(weaponPickup, _characterInventory, _characterWeaponSlot))
        {
            _characterDefinition.EquipWeapon(weaponPickup, _characterInventory, _characterWeaponSlot);
        }
    }

    public void ChangeArmor(ItemPickUp armorPickup)
    {
        if (!_characterDefinition.UnEquipArmor(armorPickup, _characterInventory))
        {
            _characterDefinition.EquipArmor(armorPickup, _characterInventory);
        }
    }

    //REPORTERS
    public int GetHealth()
    {
        return _characterDefinition.currentHealth;
    }

    public ItemPickUp GetCurrentWeapon()
    {
        return _characterDefinition.Weapon;
    }

    //GET ANYTHING ELSE NEED HERE...
}

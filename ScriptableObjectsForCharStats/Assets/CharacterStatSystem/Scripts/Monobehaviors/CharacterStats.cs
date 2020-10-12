using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    [SerializeField] private CharacterStats_SO characterDefinition;
    [SerializeField] private CharacterInventory characterInventory;
    [SerializeField] private GameObject _characterWeaponSlot;

    //CONSTRUCTORS
    public CharacterStats()
    {
        characterInventory = CharacterInventory.Instance;
    }


    //INITIALIZATIONS
    private void Start()
    {
        if (!characterDefinition.setManually)
        {
            characterDefinition.maxHealth = 100;
            characterDefinition.currentHealth = 50;

            characterDefinition.maxMana = 25;
            characterDefinition.currentMana = 10;

            characterDefinition.maxWealth = 500;
            characterDefinition.currentWealth = 0;

            characterDefinition.baseResistance = 0;
            characterDefinition.currentResistance = 0;

            characterDefinition.maxEncumbrance = 50f;
            characterDefinition.currentResistance = 0f;

            characterDefinition.charExperience = 0;
            characterDefinition.charLevel = 1;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Possible items we can have in the game
public enum ItemTypeDefinitions
{
    HEALTH, WEALTH, MANA, WEAPON, ARMOR, BUFF, EMPTY
}

public enum ItemArmorSubtype
{
    NONE, HEAD, CHEST, HANDS, LEGS, BOOTS
}

[CreateAssetMenu(fileName = "NewItem", menuName = "Spawnable Item/New Pick-up", order = 1)]
public class ItemPickUp_SO : ScriptableObject
{
    public ItemTypeDefinitions itemType = ItemTypeDefinitions.HEALTH;
    public ItemArmorSubtype itemArmorSubType = ItemArmorSubtype.NONE;
    public int itemAmount = 0;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemTypeDefinitions { HEALTH, WEALTH, MANA, WEAPON, ARMOR, BUFF, EMPTY };
public enum ItemArmorSubType { None, Head, Chest, Hands, Legs, Boots };

[CreateAssetMenu(fileName = "NewItem", menuName = "Spawnable Item/New Pick-up", order = 1)]
public class ItemPickUps_SO : ScriptableObject
{
    public string itemName = "New Item";
    public ItemTypeDefinitions itemType = ItemTypeDefinitions.HEALTH;
    public ItemArmorSubType itemArmorSubType = ItemArmorSubType.None;
    public int itemAmount = 0;
    public int spawnChanceWeight = 0; //probability to be spawned

    public Material itemMaterial = null; //material to plaster on the item
    public Sprite itemIcon = null; //for inventory menu - we got x number of swords and have icon to show it's a sword
    public Rigidbody itemSpawnObject = null;
    public Rigidbody weaponSlotObject = null;

    public bool isEquipped = false; //can be equiped
    public bool isInteractable = false; //can be interacted with
    public bool isStorable = false; //can we store this inventory or not
    public bool isUnique = false; //is this a unique item
    public bool isIndustructible = false;
    public bool isQuestItem = false;
    public bool isStackable = false; //can we stack on top of others
    public bool destroyOnUse = false; //will destroy upon usage
    public float itemWeight = 0f; //how heavy is this item
}

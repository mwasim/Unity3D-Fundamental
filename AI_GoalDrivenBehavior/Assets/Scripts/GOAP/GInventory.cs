using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GInventory
{
    private List<GameObject> _items = new List<GameObject>();

    public void AddItem(GameObject i)
    {
        _items.Add(i);
    }

    public GameObject FindItemWithTag(string tag)
    {
        foreach (var item in _items)
        {
            if (item.tag == tag)
            {
                return item;
            }
        }

        return null;
    }

    public void RemoveItem(GameObject i)
    {
        var index = -1;
        foreach (var item in _items)
        {
            index++;
            if (item == i) break;
        }

        if (index >= 0)
            _items.RemoveAt(index);
    }
}

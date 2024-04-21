using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Dictionary<Item, int> inventory = new();

    void OnEnable()
    {
        EventManager.Current.LootEvent += OnLoot;
    }
    void OnDisable()
    {
        EventManager.Current.LootEvent -= OnLoot;
    }
    
    void OnLoot(GameObject looter, LootInfo lootInfo)
    {
        if(looter!=gameObject) return;

        AddItem(lootInfo.item, lootInfo.quantity);
    }

    public void AddItem(Item item, int quantity)
    {
        if(HasItem(item))
        {
            inventory[item] += quantity;
        }
        else if(quantity>0)
        {
            inventory.Add(item, quantity);
        }
    }
    
    public void RemoveItem(Item item, int quantity)
    {
        if(!HasItem(item)) return;

        inventory[item] -= quantity;

        if(inventory[item]<=0)
        {
            inventory.Remove(item);
        }
    }

    public bool HasItem(Item item, int quantity=1)
    {
        return inventory.ContainsKey(item) && inventory[item]>=quantity;
    }

    public int GetItemQuantity(Item item)
    {
        if(!HasItem(item)) return 0;

        return inventory[item];
    }
    
    public void Clear()
    {
        inventory.Clear();
    }

    public void Drop(Item item, int quantity)
    {
        if(!HasItem(item)) return;

        int left = inventory[item];

        for(int i=0; i<quantity; i++)
        {
            if(left>0)
            {
                left--;
                ItemManager.Current.Spawn(item, transform.position);
            }
            else break;
        }

        RemoveItem(item, quantity);
    }

    public void DropAll()
    {
        foreach(Item item in inventory.Keys)
        {
            Drop(item, inventory[item]);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Item
{
    None,
    WoodLog,
    WoodPickaxe,
    Stone,
    StonePickaxe,
    CoalOre,
    IronOre,
    IronIngot,
    IronPickaxe,
    Diamond,
    DiamondPickaxe,
    Arrow,
    DiamondBlock,
    WoodAxe,
    StoneAxe,
    IronAxe,
    DiamondAxe,
    WoodSword,
    StoneSword,
    IronSword,
    DiamondSword,
    Bow,
    String,
    Stick,
    WoodPlanks,
    RottenFlesh,
    Bone,
    SpiderEye,
    GenericFood,
    SpeedPotion,
}

[System.Serializable]
public class ItemFood
{
    public string name;
    public Item item;
    public float heal;
}

[System.Serializable]
public class Potion
{
    public string name;
    public Item item;
    public Buff buff;
    public float duration;
}

public class ItemManager : MonoBehaviour
{
    public static ItemManager Current;

    void Awake()
    {
        Current=this;
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public GameObject lootPrefab;

    public GameObject Spawn(Vector3 pos, Item item, int quantity=1)
    {
        GameObject spawned = Instantiate(lootPrefab, pos, Quaternion.identity);

        if(spawned.TryGetComponent(out Loot2D loot))
        {
            loot.item = item;
            loot.quantity = quantity;
        }

        return spawned;
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public List<ItemFood> foods = new();

    public ItemFood GetFood(Item item)
    {
        foreach(ItemFood food in foods)
        {
            if(food.item==item)
            {
                return food;
            }
        }
        return null;
    }

    public List<Potion> potions = new();

    public Potion GetPotion(Item item)
    {
        foreach(Potion potion in potions)
        {
            if(potion.item==item)
            {
                return potion;
            }
        }
        return null;
    }
}

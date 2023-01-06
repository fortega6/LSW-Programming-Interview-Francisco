using UnityEngine;
using System.Collections.Generic;


[System.Serializable]
public class InventoryItem
{
    public ItemOutfitSO item;
    public int amount;

    public InventoryItem(ItemOutfitSO item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }
}

[CreateAssetMenu(fileName = "NewInventory", menuName = "Scriptable Objects/Inventory/Inventory")]
public class InventorySO : ScriptableObject
{
    [Header("Gold")]
    public int gold = 0;

    [Header("Outfits")]
    public List<InventoryItem> outfits = new List<InventoryItem>(2);


    // Public functions

    public void AddGold(int gold)
    {
        this.gold += Mathf.Abs(gold);
    }

    public void GetGold(int gold)
    {
        this.gold -= Mathf.Abs(gold);
    }

    public bool AddConsumable(ItemOutfitSO consumable)
    {
        var consumableFound = this.outfits.Find((c) => { return c.item.itemName == consumable.itemName; });

        if (consumableFound != null)
        {
            consumableFound.amount += 1;
        }
        else
        {
            var newInventoryConsumable = new InventoryItem(consumable, 1);
            this.outfits.Add(newInventoryConsumable);
        }

        return true;
    }

    public bool RemoveConsumable(ItemOutfitSO consumable)
    {
        var consumableFound = this.outfits.Find((c) => { return c.item.itemName == consumable.itemName; });

        if (consumableFound != null)
        {
            consumableFound.amount -= 1;

            if (consumableFound.amount == 0)
            {
                return true;
                //return this.consumables.Remove(consumableFound);
            }
        }

        return false;
    }
}

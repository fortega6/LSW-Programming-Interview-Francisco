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

    public bool findItem(ItemOutfitSO outfit)
    {
        return this.outfits.Find((c) => { return c.item.itemName == outfit.itemName; }) != null;
    }

    public bool AddOutfit(ItemOutfitSO outfit)
    {
        var outfitFound = this.outfits.Find((c) => { return c.item.itemName == outfit.itemName; });

        if (outfitFound != null)
        {
            outfitFound.amount += 1;

            if (!outfitFound.item.active)
            {
                if (CountOutfitsActives() < outfits.Count)
                {
                    Swap(CountOutfitsActives(), getItemId(outfit));
                    outfitFound.item.active = true;
                }
            }
        }
        else
        {
            var newInventoryOutfit = new InventoryItem(outfit, 1);
            this.outfits.Add(newInventoryOutfit);

            if (CountOutfitsActives() < outfits.Count - 1)
            {
                Swap(CountOutfitsActives(), getItemId(outfit));
            }
            newInventoryOutfit.item.active = true;
        }

        return true;
    }

    public int CountOutfitsActives()
    {
        int count = 0;
        foreach (var outfit in outfits)
        {
            if (outfit.item.active)
                count++;
        }
        return count;
    }
    public bool RemoveOutfit(ItemOutfitSO outfit)
    {
        var OutfitFound = this.outfits.Find((c) => { return c.item.itemName == outfit.itemName; });

        if (OutfitFound != null)
        {
            OutfitFound.amount -= 1;

            if (OutfitFound.amount == 0)
            {
                return true;
                //return this.Outfits.Remove(OutfitFound);
            }
        }

        return false;
    }

    public void MoveToEnd(int itemId)
    {
        if (itemId < outfits.Count)
        {
            var tmpOutfit = this.outfits[itemId]; 
            for (int i = itemId; i < outfits.Count - 1; i++)
            {
                outfits[i] = this.outfits[i+1];
            }
            this.outfits[outfits.Count-1] = tmpOutfit;
        }
    }

    public void Swap(int pos1, int pos2)
    {
        var temp = outfits[pos1]; // Copy the first position's element
        outfits[pos1] = outfits[pos2]; // Assign to the second element
        outfits[pos2] = temp; // Assign to the first element
    }

    public int getItemId(ItemSO item)
    {
        int id = 0;

        foreach(var outfit in outfits)
        {
            if (item.name == outfit.item.name)
            {
                return id;
            }
            id++;
        }

        return id;
    }
}

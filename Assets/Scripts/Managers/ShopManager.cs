using ScriptableObjectArchitecture;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShopManager : MonoBehaviour
{
    [Header("Dependencies")]
    public ShopUI shopUI;
    public InventorySO playerInventory;

    [Header("Broadcasting on channels")]
    public IntGameEvent changeOutfitRequest;


    [Header("Shop Configuration")]
    public List<int> weaponPrices = new List<int>(2);
    public List<int> consumablePrices = new List<int>(2);

    [Header("Action events")]
    public UnityEvent onShopOpened;
    public UnityEvent onShopClosed;

    private InventorySO _shopInventory;

    public void OpenShop(InventorySO shopInventory)
    {
        if (this._shopInventory != null)
        {
            CloseShop();
            return;
        }

        this._shopInventory = shopInventory;
        this.shopUI.SetupHUD(this._shopInventory, this.weaponPrices, this.consumablePrices, this.playerInventory);

        if (this.onShopOpened != null)
            this.onShopOpened.Invoke();

        shopUI.gameObject.SetActive(true);
    }

    public void CloseShop()
    {
        this._shopInventory = null;

        if (this.onShopClosed != null)
            this.onShopClosed.Invoke();

        shopUI.gameObject.SetActive(false);
    }

    public void BuyItem(int itemId)
    {
        var itemPrice = this.consumablePrices[itemId];

        if (this.playerInventory.gold < itemPrice || itemId >= this._shopInventory.outfits.Count) // No money no shopping
            return;

        var shopItem = this._shopInventory.outfits[itemId];
        this.playerInventory.GetGold(itemPrice);
        this.playerInventory.AddConsumable(shopItem.item);
        this._shopInventory.RemoveConsumable(shopItem.item);
    }

    public void OnSetOutfit(int index)
    {
        changeOutfitRequest.Raise(index);
    }

}

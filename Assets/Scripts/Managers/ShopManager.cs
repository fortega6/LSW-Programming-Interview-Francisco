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
    public BoolGameEvent dialogRequest;


    [Header("Shop Configuration")]
    public List<int> outfitPrices = new List<int>(2);

    [Header("Action events")]
    public UnityEvent onShopOpened;
    public UnityEvent onShopClosed;

    private InventorySO _shopInventory;

    private int _currItemId;

    public void OpenShop(InventorySO shopInventory)
    {
        if (this._shopInventory != null)
            return;


        this._shopInventory = shopInventory;
        this.shopUI.SetupHUD(this._shopInventory, this.outfitPrices, this.playerInventory);

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
        dialogRequest.Raise(false);
    }

    public void BuyItem(int itemId)
    {
        var itemPrice = this.outfitPrices[itemId];
        var shopItem = this._shopInventory.outfits[itemId];

        if (this.playerInventory.gold < itemPrice || itemId >= this._shopInventory.outfits.Count) // No money no shopping
            return;

        if (!shopItem.item.active && this.playerInventory.CountOutfitsActives() >= this.shopUI.playerInventoryUI.Length)
            return;

        this.playerInventory.GetGold(itemPrice);
        this.playerInventory.AddOutfit(shopItem.item);
        this._shopInventory.RemoveOutfit(shopItem.item);
        this._currItemId = itemId;
    }

    public void RemoveItem(int itemId)
    {
        var shopItem = this.playerInventory.outfits[itemId];
        shopItem.item.active = false;
        this.playerInventory.MoveToEnd(itemId);
    }

    public void OnSetOutfit(int itemId)
    {
        changeOutfitRequest.Raise(itemId);
    }
}

using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ShopUI : MonoBehaviour
{
    // Shop Inventory

    [System.Serializable]
    public class ShopInventoryUI
    {
        public Button shopFirstItemButton;
        public Image shopFirstOutfitImage;
        public TextMeshProUGUI shopFirstOutfitAmountText;
        public TextMeshProUGUI shopFirstOutfitNameText;
        public TextMeshProUGUI shopFirstOutfitCostText;
    }

    [Header("Dependencies")]
    public ShopInventoryUI[] shopInventory;


    // Player Inventory

    [System.Serializable]
    public class PlayerInventoryUI
    {
        public Button playerFirstOutfitButton;
        public Image playerFirstOutfitImage;
        public TextMeshProUGUI playerFirstOutfitAmountText;
        public Button removeOutfitButton;
    }

    public PlayerInventoryUI[] playerInventoryUI;
    public TextMeshProUGUI playerGoldText;

    private InventorySO _shopInventory;
    private List<int> _outfitPrices;
    private InventorySO _playerInventory;

    // Public functions

    public void SetupHUD(InventorySO shopInventory, List<int> outfitPrices, InventorySO playerInventory)
    {
        this.ResetShopHUD();
        this.ResetPlayerHUD();

        this._shopInventory = shopInventory;
        this._outfitPrices = outfitPrices;
        this._playerInventory = playerInventory;

        // Disable buttons just in case

        foreach (ShopInventoryUI inventory in this.shopInventory)
        {
            inventory.shopFirstItemButton.interactable = false;
        }

        // Shop items
        this.ConfigureShopOutfits();

        // Player items
        this.ConfigurePlayerOutfits();
        this.ConfigurePlayerGold();
    }



    // Private

    private void Update()
    {
        if (this._shopInventory == null || this._playerInventory == null)
            return;

        this.ConfigureShopOutfits();

        this.ConfigurePlayerOutfits();
        this.ConfigurePlayerGold();
    }

    private void ConfigureShopOutfits()
    {
        for (int index = 0; index < this._shopInventory.outfits.Count; index++)
        {
            var intem = shopInventory[index];
            var outfitItem = this._shopInventory.outfits[index];
            var outfitPrice = this._outfitPrices[index];

            intem.shopFirstOutfitImage.sprite = outfitItem.item.icon;
            intem.shopFirstOutfitImage.color = Color.white;
            intem.shopFirstOutfitNameText.text = outfitItem.item.itemName;
            intem.shopFirstOutfitAmountText.text = outfitItem.amount.ToString();
            intem.shopFirstOutfitCostText.text = outfitPrice.ToString();

            intem.shopFirstItemButton.interactable = (outfitItem.amount > 0);
        }
    }

    private void ConfigurePlayerOutfits()
    {
        int i = 0;
        foreach (var inventoryUI in playerInventoryUI)
        {
            inventoryUI.playerFirstOutfitImage.sprite = null;
            inventoryUI.playerFirstOutfitImage.color = Color.clear;
            inventoryUI.playerFirstOutfitAmountText.text = null;
            inventoryUI.playerFirstOutfitButton.interactable = false;
            inventoryUI.removeOutfitButton.interactable = false;

            if (i < this._playerInventory.outfits.Count)
            {
                // Player has 1 outfit
                var outfitItem = this._playerInventory.outfits[i];
                if (outfitItem.item.active)
                {
                    inventoryUI.playerFirstOutfitImage.sprite = outfitItem.item.icon;
                    inventoryUI.playerFirstOutfitImage.color = Color.white;
                    inventoryUI.playerFirstOutfitAmountText.text = outfitItem.amount.ToString();
                    inventoryUI.removeOutfitButton.interactable = inventoryUI.playerFirstOutfitButton.interactable = (outfitItem.amount > 0);
                }
            }
            i++;
        }
    }

    private void ConfigurePlayerGold()
    {
        this.playerGoldText.text = this._playerInventory.gold.ToString();
    }

    private void ResetShopHUD()
    {
        foreach (ShopInventoryUI inventory in shopInventory)
        {

            inventory.shopFirstOutfitImage.sprite = null;
            inventory.shopFirstOutfitImage.color = Color.clear;
            inventory.shopFirstOutfitNameText.text = "-";
            inventory.shopFirstOutfitCostText.text = "-";
            inventory.shopFirstItemButton.interactable = false;
        }
    }

    private void ResetPlayerHUD()
    {
        foreach (PlayerInventoryUI inventory in playerInventoryUI)
        {
            inventory.playerFirstOutfitImage.sprite = null;
            inventory.playerFirstOutfitImage.color = Color.clear;
            inventory.playerFirstOutfitAmountText.text = null;
        }

        this.playerGoldText.text = "-";
    }
}

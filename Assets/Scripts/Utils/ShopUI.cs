using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    // Shop Inventory

    [System.Serializable]
    public class ShopInventoryUI
    {
        public Button shopFirstConsumableButton;
        public Image shopFirstConsumableImage;
        public TextMeshProUGUI shopFirstConsumableAmountText;
        public TextMeshProUGUI shopFirstConsumableNameText;
        public TextMeshProUGUI shopFirstConsumableCostText;
    }

    [Header("Dependencies")]
    public ShopInventoryUI[] shopInventory;


    // Player Inventory

    [System.Serializable]
    public class PlayerInventoryUI
    {
        public Button playerFirstConsumableButton;
        public Image playerFirstConsumableImage;
        public TextMeshProUGUI playerFirstConsumableAmountText;
    }

    public PlayerInventoryUI[] playerInventoryUI;
    public TextMeshProUGUI playerGoldText;


    private InventorySO _shopInventory;
    private List<int> _weaponPrices;
    private List<int> _consumablePrices;
    private InventorySO _playerInventory;

    // Public functions

    public void SetupHUD(InventorySO shopInventory, List<int> weaponPrices, List<int> consumablePrices, InventorySO playerInventory)
    {
        this.ResetShopHUD();
        this.ResetPlayerHUD();

        this._shopInventory = shopInventory;
        this._weaponPrices = weaponPrices;
        this._consumablePrices = consumablePrices;
        this._playerInventory = playerInventory;

        // Disable buttons just in case

        foreach (ShopInventoryUI inventory in this.shopInventory)
        {
            inventory.shopFirstConsumableButton.interactable = false;
        }

        // Shop items
        this.ConfigureShopConsumables();

        // Player items
        this.ConfigurePlayerConsumables();
        this.ConfigurePlayerGold();
    }



    // Private

    private void Update()
    {
        if (this._shopInventory == null || this._playerInventory == null)
            return;

        this.ConfigureShopConsumables();

        this.ConfigurePlayerConsumables();
        this.ConfigurePlayerGold();
    }

    private void ConfigureShopConsumables()
    {
        for (int index = 0; index < this._shopInventory.outfits.Count; index++)
        {
            var intem = shopInventory[index];
            var consumableItem = this._shopInventory.outfits[index];
            var consumablePrice = this._consumablePrices[index];

            intem.shopFirstConsumableImage.sprite = consumableItem.item.icon;
            intem.shopFirstConsumableImage.color = Color.white;
            intem.shopFirstConsumableNameText.text = consumableItem.item.itemName;
            intem.shopFirstConsumableAmountText.text = consumableItem.amount.ToString();
            intem.shopFirstConsumableCostText.text = consumablePrice.ToString();

            intem.shopFirstConsumableButton.interactable = (consumableItem.amount > 0);
        }
    }

    private void ConfigurePlayerConsumables()
    {
        int index = 0;
        foreach (PlayerInventoryUI inventory in playerInventoryUI)
        {
            if (index < this._playerInventory.outfits.Count)
            {
                // Player has 1 weapon
                var consumableItem = this._playerInventory.outfits[index];

                inventory.playerFirstConsumableImage.sprite = consumableItem.item.icon;
                inventory.playerFirstConsumableImage.color = Color.white;
                inventory.playerFirstConsumableAmountText.text = consumableItem.amount.ToString();
                inventory.playerFirstConsumableButton.interactable = (consumableItem.amount > 0);
            }
            else
            {
                inventory.playerFirstConsumableImage.sprite = null;
                inventory.playerFirstConsumableImage.color = Color.clear;
                inventory.playerFirstConsumableAmountText.text = null;
                inventory.playerFirstConsumableButton.interactable = false;
            }
            index++;
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

            inventory.shopFirstConsumableImage.sprite = null;
            inventory.shopFirstConsumableImage.color = Color.clear;
            inventory.shopFirstConsumableNameText.text = "-";
            inventory.shopFirstConsumableCostText.text = "-";
            inventory.shopFirstConsumableButton.interactable = false;
        }
    }



    private void ResetPlayerHUD()
    {
        foreach (PlayerInventoryUI inventory in playerInventoryUI)
        {
            inventory.playerFirstConsumableImage.sprite = null;
            inventory.playerFirstConsumableImage.color = Color.clear;
            inventory.playerFirstConsumableAmountText.text = null;
        }

        this.playerGoldText.text = "-";
    }
}

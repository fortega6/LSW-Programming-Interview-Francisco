using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [System.Serializable]
    public class ShopInventoryUI
    {
        public Button shopFirstConsumableButton;
        public Image shopFirstConsumableImage;
        public TextMeshProUGUI shopFirstConsumableAmountText;
        public TextMeshProUGUI shopFirstConsumableNameText;
        public TextMeshProUGUI shopFirstConsumableCostText;
    }

    public ShopInventoryUI[] shopInventory;

    [Header("Dependencies")]

    // Shop Inventory
    public Button shopFirstWeaponButton;
    public Image shopFirstWeaponImage;
    public TextMeshProUGUI shopFirstWeaponNameText;
    public TextMeshProUGUI shopFirstWeaponCostText;

    public Button shopSecondWeaponButton;
    public Image shopSecondWeaponImage;
    public TextMeshProUGUI shopSecondWeaponNameText;
    public TextMeshProUGUI shopSecondWeaponCostText;

    public Button shopFirstConsumableButton;
    public Image shopFirstConsumableImage;
    public TextMeshProUGUI shopFirstConsumableAmountText;
    public TextMeshProUGUI shopFirstConsumableNameText;
    public TextMeshProUGUI shopFirstConsumableCostText;

    public Button shopSecondConsumableButton;
    public Image shopSecondConsumableImage;
    public TextMeshProUGUI shopSecondConsumableAmountText;
    public TextMeshProUGUI shopSecondConsumableNameText;
    public TextMeshProUGUI shopSecondConsumableCostText;

    public Button shopThirdConsumableButton;
    public Image shopThirConsumableImage;
    public TextMeshProUGUI shopThirConsumableAmountText;
    public TextMeshProUGUI shopThirConsumableNameText;
    public TextMeshProUGUI shopThirConsumableCostText;

    public Button shopFourthConsumableButton;
    public Image shopFourthConsumableImage;
    public TextMeshProUGUI shopFourthConsumableAmountText;
    public TextMeshProUGUI shopFourthConsumableNameText;
    public TextMeshProUGUI shopFourthConsumableCostText;

    public Button shopFifthConsumableButton;
    public Image shopFifthConsumableImage;
    public TextMeshProUGUI shopFifthConsumableAmountText;
    public TextMeshProUGUI shopFifthConsumableNameText;
    public TextMeshProUGUI shopFifthConsumableCostText;

    public Button shopSixthConsumableButton;
    public Image shopSixthConsumableImage;
    public TextMeshProUGUI shopSixthConsumableAmountText;
    public TextMeshProUGUI shopSixthConsumableNameText;
    public TextMeshProUGUI shopSixthConsumableCostText;


    // Player Inventory

    [System.Serializable]
    public class PlayerInventoryUI
    {
        public Button playerFirstConsumableButton;
        public Image playerFirstConsumableImage;
        public TextMeshProUGUI playerFirstConsumableAmountText;
    }

    public PlayerInventoryUI[] playerInventoryUI;

    public Image playerFirstWeaponImage;

    public Image playerSecondWeaponImage;

    public Image playerFirstConsumableImage;
    public TextMeshProUGUI playerFirstConsumableAmountText;

    public Image playerSecondConsumableImage;
    public TextMeshProUGUI playerSecondConsumableAmountText;

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
        this.shopFirstWeaponButton.interactable = false;
        this.shopSecondWeaponButton.interactable = false;
        this.shopFirstConsumableButton.interactable = false;
        this.shopSecondConsumableButton.interactable = false;

        // Shop items
        this.ConfigureShopWeapons();
        this.ConfigureShopConsumables();

        // Player items
        this.ConfigurePlayerWeapons();
        this.ConfigurePlayerConsumables();
        this.ConfigurePlayerGold();
    }



    // Private

    private void Update()
    {
        if (this._shopInventory == null || this._playerInventory == null)
            return;

        this.ConfigureShopConsumables();

        this.ConfigurePlayerWeapons();
        this.ConfigurePlayerConsumables();
        this.ConfigurePlayerGold();
    }



    private void ConfigureShopWeapons()
    {
        for (int weaponIndex = 0; weaponIndex < this._shopInventory.weapons.Count; weaponIndex++)
        {
            var weaponItem = this._shopInventory.weapons[weaponIndex];
            var weaponPrice = this._weaponPrices[weaponIndex];

            if (weaponIndex == 0)
            {
                this.shopFirstWeaponImage.sprite = weaponItem.icon;
                this.shopFirstWeaponImage.color = Color.white;
                this.shopFirstWeaponNameText.text = weaponItem.itemName;
                this.shopFirstWeaponCostText.text = weaponPrice.ToString();
                this.shopFirstWeaponButton.interactable = true;
            }
            if (weaponIndex == 1)
            {
                this.shopSecondWeaponImage.sprite = weaponItem.icon;
                this.shopSecondWeaponImage.color = Color.white;
                this.shopSecondWeaponNameText.text = weaponItem.itemName;
                this.shopSecondWeaponCostText.text = weaponPrice.ToString();
                this.shopSecondWeaponButton.interactable = true;
            }
        }
    }



    private void ConfigureShopConsumables()
    {
        for (int index = 0; index < this._shopInventory.consumables.Count; index++)
        {
            var intem = shopInventory[index];
            var consumableItem = this._shopInventory.consumables[index];
            var consumablePrice = this._consumablePrices[index];

            intem.shopFirstConsumableImage.sprite = consumableItem.item.icon;
            intem.shopFirstConsumableImage.color = Color.white;
            intem.shopFirstConsumableNameText.text = consumableItem.item.itemName;
            intem.shopFirstConsumableAmountText.text = consumableItem.amount.ToString();
            intem.shopFirstConsumableCostText.text = consumablePrice.ToString();

            intem.shopFirstConsumableButton.interactable = (consumableItem.amount > 0);
        }
    }



    private void ConfigurePlayerWeapons()
    {
        if (this._playerInventory.weapons.Count > 1)
        {
            // Player has 2 weapons
            var weaponItem = this._playerInventory.weapons[1];

            this.playerSecondWeaponImage.sprite = weaponItem.icon;
            this.playerSecondWeaponImage.color = Color.white;
        }
        else
        {
            this.playerSecondWeaponImage.sprite = null;
            this.playerSecondWeaponImage.color = Color.clear;
        }

        if (this._playerInventory.weapons.Count > 0)
        {
            // Player has 1 weapon
            var weaponItem = this._playerInventory.weapons[0];

            this.playerFirstWeaponImage.sprite = weaponItem.icon;
            this.playerFirstWeaponImage.color = Color.white;
        }
        else
        {
            this.playerFirstWeaponImage.sprite = null;
            this.playerFirstWeaponImage.color = Color.clear;
        }
    }



    private void ConfigurePlayerConsumables()
    {
        int index = 0;
        foreach (PlayerInventoryUI inventory in playerInventoryUI)
        {
            if (index < this._playerInventory.consumables.Count)
            {
                // Player has 1 weapon
                var consumableItem = this._playerInventory.consumables[index];

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
        this.shopFirstWeaponImage.sprite = null;
        this.shopFirstWeaponImage.color = Color.clear;
        this.shopFirstWeaponNameText.text = "-";
        this.shopFirstWeaponCostText.text = "-";
        this.shopFirstWeaponButton.interactable = false;

        this.shopSecondWeaponImage.sprite = null;
        this.shopSecondWeaponImage.color = Color.clear;
        this.shopSecondWeaponNameText.text = "-";
        this.shopSecondWeaponCostText.text = "-";
        this.shopSecondWeaponButton.interactable = false;

        this.shopFirstConsumableImage.sprite = null;
        this.shopFirstConsumableImage.color = Color.clear;
        this.shopFirstConsumableNameText.text = "-";
        this.shopFirstConsumableAmountText.text = "-";
        this.shopFirstConsumableCostText.text = "-";
        this.shopFirstConsumableButton.interactable = false;

        this.shopSecondConsumableImage.sprite = null;
        this.shopSecondConsumableImage.color = Color.clear;
        this.shopSecondConsumableNameText.text = "-";
        this.shopSecondConsumableAmountText.text = "-";
        this.shopSecondConsumableCostText.text = "-";
        this.shopSecondConsumableButton.interactable = false;
    }



    private void ResetPlayerHUD()
    {
        this.playerFirstWeaponImage.sprite = null;
        this.playerFirstWeaponImage.color = Color.clear;

        this.playerSecondWeaponImage.sprite = null;
        this.playerSecondWeaponImage.color = Color.clear;

        this.playerFirstConsumableImage.sprite = null;
        this.playerFirstConsumableImage.color = Color.clear;
        this.playerFirstConsumableAmountText.text = null;

        this.playerSecondConsumableImage.sprite = null;
        this.playerSecondConsumableImage.color = Color.clear;
        this.playerSecondConsumableAmountText.text = null;

        this.playerGoldText.text = "-";
    }
}

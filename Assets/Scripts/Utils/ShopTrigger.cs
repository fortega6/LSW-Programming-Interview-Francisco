using UnityEngine;
using ScriptableObjectArchitecture;

public class ShopTrigger : MonoBehaviour
{
    [Header("Configuration")]
    public InventorySO shopInventory;

    [Header("Broadcasting events")]
    public InventorySOGameEvent shopRequestEvent;

    public void TriggerShop()
    {
        Debug.Log("Interact");
        this.shopRequestEvent.Raise(this.shopInventory);
    }
}

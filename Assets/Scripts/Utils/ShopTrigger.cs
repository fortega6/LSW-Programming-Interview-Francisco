using UnityEngine;
using ScriptableObjectArchitecture;

public class ShopTrigger : MonoBehaviour
{
    [Header("Configuration")]
    public InventorySO shopInventory;

    [Header("Broadcasting events")]
    public InventorySOGameEvent shopRequestEvent;

    public Animator animator;

    public void TriggerShop()
    {
        Debug.Log("Interact");
        this.shopRequestEvent.Raise(this.shopInventory);
    }

    public void TirggerTakingDialog()
    {
        Debug.Log(animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"));
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            animator.Play("Talking");
        }
        else
        {
            animator.Play("Idle");
        }
    }
}

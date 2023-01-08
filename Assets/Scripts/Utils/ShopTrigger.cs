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
        Debug.Log("TriggerShop");
        this.shopRequestEvent.Raise(this.shopInventory);
    }

    public void TirggerTakingDialog(bool isTalking)
    {
        if (isTalking)
        {
            animator.Play("Talking");
        }
        else
        {
            animator.Play("Idle");
        }
    }

    public void TirggerTakingDialog()
    {
        Debug.Log("TirggerTakingDialog " + animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"));
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

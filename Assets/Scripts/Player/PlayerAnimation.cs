using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{
    [Header("Dependencies")]
    public Animator animator;

    public InventorySO playerInventory;

    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 movementInput = value.ReadValue<Vector2>();

        animator.SetFloat("Speed", movementInput.sqrMagnitude);
        animator.SetFloat("Horizontal", movementInput.x);
        animator.SetFloat("Vertical", movementInput.y);
    }

    public void OnSetSkinRequest(int index)
    {
        var animatorOverrideController = new AnimatorOverrideController(playerInventory.consumables[index].item.animator);
        animator.runtimeAnimatorController = animatorOverrideController; ;
    }
}

using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [Header("Action events")]
    public UnityEvent onInteract;

    public bool isAutoInteract = false;

    public void Interact()
    {
        Debug.Log("Interact");

        if (onInteract != null)
            onInteract.Invoke();
    }
}

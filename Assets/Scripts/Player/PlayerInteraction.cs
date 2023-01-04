using UnityEngine;
using ScriptableObjectArchitecture;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Configuration")]
    public string interactableTag;

    //[Header("Broadcasting events")]
    //public BoolGameEvent interactionRequestEvent;

    private Interactable _interactable;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(interactableTag))
        {
            Debug.Log("name" + collision.gameObject.name);
            var interactable = collision.GetComponent<Interactable>();
            this._interactable = interactable;

            if (_interactable.isAutoInteract)
            {
                EnableInteractable();
            }
        }

        //this.interactionRequestEvent.Raise((_interactable != null));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_interactable == null)
            return;

        if (collision.CompareTag(interactableTag))
        {
            if (_interactable.isAutoInteract)
            {
                EnableInteractable();
            }
            this._interactable = null;
        }

        //this.interactionRequestEvent.Raise((_interactable != null));
    }

    public void EnableInteractable()
    {
        if (this._interactable != null)
            this._interactable.Interact();
    }

}

using UnityEngine;
using ScriptableObjectArchitecture;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Configuration")]
    public string interactableTag;

    //[Header("Broadcasting events")]
    //public BoolGameEvent interactionRequestEvent;

    public Animator animator;

    private Interactable _interactable;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(interactableTag))
        {
            ///Debug.Log("name" + collision.gameObject.name);
            var interactable = collision.GetComponent<Interactable>();
            this._interactable = interactable;

            if (this._interactable.isAutoInteract)
            {
                EnableInteractable();
            }
            else
            {
                this.animator.gameObject.SetActive(true);
            }
        }

        //this.interactionRequestEvent.Raise((_interactable != null));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("1 OnTriggerExit2D" + _interactable);
        if (_interactable == null)
            return;
        Debug.Log("2 OnTriggerExit2D" + _interactable);

        if (collision.CompareTag(interactableTag))
        {
            if (this._interactable.isAutoInteract)
            {
                EnableInteractable();
            }
            else
            {
                this.animator.gameObject.SetActive(false);
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

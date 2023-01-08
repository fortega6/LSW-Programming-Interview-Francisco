using UnityEngine;
using UnityEngine.InputSystem;

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
            Debug.Log("1 OnTriggerEnter2D " + collision.gameObject.name);
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
                animator.Play("Talking");
            }
        }

        //this.interactionRequestEvent.Raise((_interactable != null));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        this.animator.gameObject.SetActive(false);

        if (_interactable == null)
            return;

        if (collision.CompareTag(interactableTag))
        {
            this._interactable = null;
        }


        //this.interactionRequestEvent.Raise((_interactable != null));
    }

    public void EnableInteractable()
    {
        if (this._interactable != null)
            this._interactable.Interact();
    }

    public void EnableInteractable(InputAction.CallbackContext context)
    {
        if (context.performed && this._interactable != null)
            this._interactable.Interact();
    }
}

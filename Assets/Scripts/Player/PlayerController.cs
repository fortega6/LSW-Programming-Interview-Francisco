using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Configuration")]
    public float speed;

    [Header("Dependencies")]
    public Rigidbody2D rb;

    // Privete
    private Vector2 _movementInput;

    private void FixedUpdate()
    {
        rb.velocity = _movementInput * speed;
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        _movementInput = value.ReadValue<Vector2>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D");
        //if (other.tag == "WayPoint")
            //
    }
}

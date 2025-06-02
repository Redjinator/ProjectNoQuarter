using UnityEngine;
using UnityEngine.InputSystem; // <-- new!

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    Rigidbody2D rb;
    Vector2 moveInput; // stores input from the new system
    public Vector2 moveDir => moveInput;


    // Reference to your InputActions
    PlayerInputActions inputActions;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        inputActions = new PlayerInputActions(); // auto-generated class name
    }

    void OnEnable() {
        inputActions.Enable();
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;
    }

    void OnDisable() {
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Disable();
    }

    void OnMove(InputAction.CallbackContext ctx) {
        moveInput = ctx.ReadValue<Vector2>();
    }

    void FixedUpdate() {
        rb.linearVelocity = moveInput.normalized * moveSpeed;
    }
}


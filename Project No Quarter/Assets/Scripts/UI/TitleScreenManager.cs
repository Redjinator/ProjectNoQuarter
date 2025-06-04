using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    PlayerInputActions inputActions;

    void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Move.performed += OnAnyInput; // You can change this to a specific input if needed
    }

    void OnDisable()
    {
        inputActions.Player.Move.performed -= OnAnyInput;
        inputActions.Disable();
    }

    void OnAnyInput(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene("Game"); // Replace with your actual game scene name
    }
}


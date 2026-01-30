using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public Vector2 moveInput;
    private PlayerInputController playerController;
    public event Action OnJump = delegate{};

    public event Action OnInteract = delegate{};

    void Awake()
    {
        playerController = new PlayerInputController();
    }

    void OnEnable()
    {
        // performed dispara quando apertar o botão
        playerController.Geral.Move.performed += Move;
        playerController.Geral.Move.canceled += ResetMove;

        playerController.Geral.Jump.performed += Jump;
        playerController.Geral.Interact.performed += Interact;

        playerController.Geral.Enable();
    }
    void OnDisable()
    {
        playerController.Geral.Move.performed -= Move;
        playerController.Geral.Move.canceled -= ResetMove;

        playerController.Geral.Jump.canceled -= Jump;
        playerController.Geral.Interact.canceled -= Interact;

        playerController.Geral.Disable();


    }
    private void Move(InputAction.CallbackContext cntx)
    {
        moveInput = cntx.ReadValue<Vector2>();
    }
    private void ResetMove(InputAction.CallbackContext cntx)
    {
        moveInput = Vector2.zero;
    }

    private void Jump(InputAction.CallbackContext cntx)
    {
        OnJump?.Invoke();
    }

    private void Interact(InputAction.CallbackContext cntx)
    {
        OnInteract?.Invoke();
    }
}

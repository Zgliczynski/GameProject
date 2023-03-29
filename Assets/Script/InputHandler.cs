using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public event EventHandler OnSprintAction;

    private PlayerInputAction playerInputAction;

    private void Awake()
    {
        playerInputAction = new PlayerInputAction();
        playerInputAction.PlayerMovement.Enable();

        playerInputAction.PlayerMovement.Sprint.performed += Sprint_performed;
    }

    private void Sprint_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnSprintAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputAction.PlayerMovement.Movement.ReadValue<Vector2>();

        inputVector = inputVector.normalized;
        return inputVector;
    }
}


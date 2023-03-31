using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public PlayerInputAction playerInputAction;

    public bool sprintInput;

    public bool canSprint;


    private void Awake()
    {
        playerInputAction = new PlayerInputAction();
        playerInputAction.PlayerMovement.Enable();
    }

    public void UpdateInput()
    {
        SprintHandleInput();
    }

    private void SprintHandleInput()
    {
        //sprintInput = playerInputAction.PlayerMovement.Sprint.phase == UnityEngine.InputSystem.InputActionPhase.Started;

        if (playerInputAction.PlayerMovement.Sprint.phase == UnityEngine.InputSystem.InputActionPhase.Started)
        {
            canSprint = true;
        }
        
        if(playerInputAction.PlayerMovement.Sprint.phase == UnityEngine.InputSystem.InputActionPhase.Disabled)
        {
            canSprint = false;
        }
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputAction.PlayerMovement.Movement.ReadValue<Vector2>();

        inputVector = inputVector.normalized;
        return inputVector;
    }

}


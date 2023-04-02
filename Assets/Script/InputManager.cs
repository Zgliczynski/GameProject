using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public PlayerInputAction playerInputAction;
    private PlayerMovement playerMovement;
    private PlayerAnimator playerAnimator;

    public Vector2 movementInput;
    public float verticalInput;
    public float horizontalInput;
    public float moveAmount;

    public bool sprintInput;
    public bool jumpInput;

    private void Awake()
    {
        playerAnimator = GetComponent<PlayerAnimator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {
        if(playerInputAction == null)
        {
            playerInputAction = new PlayerInputAction();

            playerInputAction.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();

            playerInputAction.PlayerAction.Sprint.performed += i => sprintInput = true;
            playerInputAction.PlayerAction.Sprint.canceled += i => sprintInput = false;
            playerInputAction.PlayerAction.Jump.performed += i => jumpInput = true;
        }

        playerInputAction.Enable();
    }

    private void OnDisable()
    {
        playerInputAction.Disable();
    }

    public void InputManagerUpdate()
    {
        HandleMovementInput();
        HandleSprintingInput();
        HandleJumpingInput();
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        playerAnimator.UpdateAnimatorValues(0, moveAmount, playerMovement.isSprinting);
    }

    private void HandleSprintingInput()
    {
        if (sprintInput && moveAmount > 0.5f)
        {
            playerMovement.isSprinting = true;
        }
        else
        {
            playerMovement.isSprinting = false;
        }
    }

    private void HandleJumpingInput()
    {
        if (jumpInput)
        {
            jumpInput = false;
            playerMovement.HandleJumping();
        }
    }
}


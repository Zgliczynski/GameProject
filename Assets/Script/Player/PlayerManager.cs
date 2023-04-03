using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Jump is write but not implemeted to game
public class PlayerManager : MonoBehaviour
{
    private Animator animator;
    private InputManager inputManager;
    private PlayerMovement playerMovement;

    public bool isInteracting;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        inputManager = GetComponent<InputManager>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        inputManager.InputManagerUpdate();
    }

    private void FixedUpdate()
    {
        playerMovement.PlayerMovementUpdate();
    }
    private void LateUpdate()
    {
        isInteracting = animator.GetBool("isInteracting");
        animator.SetBool("isGrounded", playerMovement.isGrounded);
        //playerMovement.isJumping = animator.GetBool("isJumping");

    }
}

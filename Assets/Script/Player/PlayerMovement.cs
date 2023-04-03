using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Jump is write but not implemeted to game
public class PlayerMovement : MonoBehaviour
{
    private PlayerManager playerManager;
    private InputManager inputManager;
    private PlayerAnimator playerAnimator;

    private Rigidbody rigidbody;
    private Vector3 moveDirection;
    private Transform cameraObject;

    [Header("Movement Flags")]
    public bool isSprinting;
    public bool isGrounded;
    public bool isJumping;

    [Header("Move Stats")]
    public float walkingSpeed = 3f;
    public float runningSpeed = 5f;
    public float sprintingSpeed = 7f;
    public float rotationSpeed = 15f;

    [Header("Falling")]
    public float inAirTimer;
    public float leapingVelocity;
    public float fallingVelocity;
    public float rayCastHeighOffSet = 0.5f;
    public LayerMask groundLayer;

    /*[Header("Jumping")]
    public float jumpHeight = 3f;
    public float gravityIntensity = -15f;*/

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        inputManager = GetComponent<InputManager>();
        playerAnimator = GetComponent<PlayerAnimator>();
        rigidbody = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;

        isGrounded = true;
    }

    public void PlayerMovementUpdate()
    {
        HandleFallingAndLanding();
        if (playerManager.isInteracting)
            return;

        HandleMovement();
        HandleRotation();
    }

    #region Movement & Rotation
    private void HandleMovement()
    {
        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection = moveDirection + cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;

        if (isSprinting)
        {
            moveDirection = moveDirection * sprintingSpeed;
        }
        else
        {
            if (inputManager.moveAmount >= 0.5f)
            {
                //If we running, select the runningSpeed
                moveDirection = moveDirection * runningSpeed;
            }
            else
            {
                //If we walking, select the walkingSpeed
                moveDirection = moveDirection * walkingSpeed;
            }
        }
        
        Vector3 movementVelocity = moveDirection;
        rigidbody.velocity = movementVelocity;
        
    }

    private void HandleRotation()
    {
        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraObject.forward * inputManager.verticalInput;
        targetDirection = targetDirection + cameraObject.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
            targetDirection = transform.forward;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }
    #endregion

    #region Falling, Landing & Jumping
    private void HandleFallingAndLanding() 
    {
        RaycastHit hit;
        Vector3 rayCastOrigin = transform.position;
        Vector3 targetPosition;
        rayCastOrigin.y = rayCastOrigin.y + rayCastHeighOffSet;
        targetPosition = transform.position;

        if (!isGrounded /*&& !isJumping*/)
        {
            if (!playerManager.isInteracting)
            {
                playerAnimator.PlayTargetAnimation("Falling", true);
            }

            inAirTimer = inAirTimer + Time.deltaTime;
            rigidbody.AddForce(transform.forward * leapingVelocity);
            rigidbody.AddForce(-Vector3.up * fallingVelocity * inAirTimer);
        }

        if (Physics.SphereCast(rayCastOrigin, 0.2f, Vector3.down, out hit, 0.5f,groundLayer))
        {
            if (!isGrounded && !playerManager.isInteracting)
            {
                playerAnimator.PlayTargetAnimation("Land", true);
            }

            Vector3 rayCastHitPoint = hit.point;
            targetPosition.y = rayCastHitPoint.y;
            inAirTimer = 0;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if(isGrounded)
        {
            if (playerManager.isInteracting || inputManager.moveAmount > 0)
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime / 0.1f);
            }
            else
            {
                transform.position = targetPosition;
            }
        }
    }

    /*public void HandleJumping()
    {
       if (isGrounded)
        {
            playerAnimator.animator.SetBool("isJumping", true);
            playerAnimator.PlayTargetAnimation("Jump", false);

            float jumpingVelocity = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight);
            Vector3 playerVelocity = moveDirection;
            playerVelocity.y = jumpingVelocity;
            rigidbody.velocity = playerVelocity;
        }
    }*/
    #endregion
}

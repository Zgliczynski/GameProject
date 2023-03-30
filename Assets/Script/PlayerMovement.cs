using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float runSpeed = 7f;
    [SerializeField] InputHandler inputHandler;

    [SerializeField] private bool isWalking;
    [SerializeField] private bool isRunning;
    private Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        inputHandler.OnSprintAction += InputHandler_OnSprintAction;
    }

    private void InputHandler_OnSprintAction(object sender, System.EventArgs e)
    {
        isRunning = true;
        
    }

    private void Update()
    {
        MovementHandler();
        Run();
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    public bool IsRunning()
    {
        return isRunning;
    }
    #region Movement
    private void MovementHandler()
    {
        Vector2 inputVector = inputHandler.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 0.2f;
        float playerHight = 1.8f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHight, playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            //Cannot move towards moveDir

            //Attemp only X movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
            canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                //Can move only on X
                moveDir = moveDirX;
            }
            else
            {
                //Cannot move only on the X

                //Attemp only Z movement
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;
                canMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHight, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    //Can move only on Z
                    moveDir = moveDirZ;
                }
                else
                {
                    //Cannot move in any direction
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }

        isWalking = moveDir != Vector3.zero;

        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }
    #endregion

    private void Run()
    {
        if(isRunning == true)
        {
            moveSpeed = runSpeed;
        }
        else
        {
            moveSpeed = 3f;
            IsRunning();
        }
    }
}

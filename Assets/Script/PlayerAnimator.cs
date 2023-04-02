using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator animator;

    private int horizontal;
    private int vertical;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
    }

    public void PlayTargetAnimation(string targetAnimation, bool isInteracting)
    {
        animator.SetBool("isInteracting", isInteracting);
        animator.CrossFade(targetAnimation, 0.2f);
    }

    public void UpdateAnimatorValues(float horizontalMovement, float verticalMovement, bool isSprinting)
    {
        //Animation Snapping
        float snappedHorizontal;
        float snappefVertical;
        #region Snapped Horizontal
        if (horizontalMovement > 0 && horizontalMovement < 0.55f)
        {
            snappedHorizontal = 0.5f;
        }
        else if (horizontalMovement > 0.55f)
        {
            snappedHorizontal = 1f;
        }
        else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
        {
            snappedHorizontal = -0.5f;
        }
        else if (horizontalMovement < -0.55f)
        {
            snappedHorizontal = -1;
        }
        else
        {
            snappedHorizontal = 0;
        }
        #endregion
        #region Snapped Vertical
        if (verticalMovement > 0 && verticalMovement < 0.55f)
        {
            snappefVertical = 0.5f;
        }
        else if (verticalMovement > 0.55f)
        {
            snappefVertical = 1f;
        }
        else if (verticalMovement < 0 && verticalMovement > -0.55f)
        {
            snappefVertical = -0.5f;
        }
        else if (verticalMovement < -0.55f)
        {
            snappefVertical = -1;
        }
        else
        {
            snappefVertical = 0;
        }
        #endregion

        if (isSprinting)
        {
            snappedHorizontal = horizontalMovement;
            snappefVertical = 2;
        }

        animator.SetFloat(horizontal, snappedHorizontal, 0.1f, Time.deltaTime);
        animator.SetFloat(vertical, snappefVertical, 0.1f, Time.deltaTime);
    }
}

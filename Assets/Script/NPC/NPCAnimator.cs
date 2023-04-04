using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimator : MonoBehaviour
{
    private Animator animator;
    private NPCMovement npcMovement;

    private void Start()
    {
        animator = GetComponent<Animator>();
        npcMovement = GetComponent<NPCMovement>();
        npcMovement.velocity = Animator.StringToHash("Velocity");
    }

    private void Update()
    {
        PlayAnimation();
    }

    private void PlayAnimation()
    {
        if(npcMovement.velocity > 0)
        {
            animator.SetBool("IsWalk", npcMovement.isWalk);
        }

        if(npcMovement.velocity == 0)
        {
            animator.SetBool("IsDancing", npcMovement.isDancing);
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;
    private PlayerInputs playerInputs;

    private readonly int XMOVE = Animator.StringToHash("Xmove");
    private readonly int YMOVE = Animator.StringToHash("Ymove");
    private readonly int DanceTrigger = Animator.StringToHash("DanceTrigger");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerInputs = GetComponent<PlayerInputs>();
    }

    private void Update()
    {
        var move = playerInputs.MoveDir;

        if(playerInputs.IsSprinting)
        {
            move *= 2;
        }

        animator.SetFloat(XMOVE, Mathf.Lerp(animator.GetFloat(XMOVE), move.x, Time.deltaTime * 5));
        animator.SetFloat(YMOVE, Mathf.Lerp(animator.GetFloat(YMOVE), move.y, Time.deltaTime * 5));

        if(playerInputs.IsDanceTriggered)
        {
            animator.SetTrigger(DanceTrigger);
        }
    }
}

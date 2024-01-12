using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private List<AudioClip> danceMusicsList;

    private Animator animator;
    private PlayerInputs playerInputs;
    private PlayerMovementController playerMovement;

    private int danceIndex = 0;

    private readonly int XMOVE = Animator.StringToHash("Xmove");
    private readonly int YMOVE = Animator.StringToHash("Ymove");
    private readonly int DanceTrigger = Animator.StringToHash("DanceTrigger");
    private readonly int DanceChange = Animator.StringToHash("DanceIndex");
    private readonly int JumpTrigger = Animator.StringToHash("Jump");
    private readonly int IsGroundedBool = Animator.StringToHash("IsGrounded");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerInputs = GetComponent<PlayerInputs>();
        playerMovement = GetComponent<PlayerMovementController>();
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
            AudioSource.PlayClipAtPoint(danceMusicsList[danceIndex % 2], transform.position);
        }

        if(playerInputs.IsDanceChangeRequested)
        {
            animator.SetFloat(DanceChange, (++danceIndex) % 2);
        }

        if(playerInputs.IsJumped)
        {
            animator.SetTrigger(JumpTrigger);
        }

        animator.SetBool(IsGroundedBool, playerMovement.IsGrounded);
    }
}

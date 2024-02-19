using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private List<AudioClip> danceMusicsList;

    private Animator animator;
    private PlayerInputs playerInputs;
    private PlayerMovementController playerMovement;

    private readonly int XMOVE = Animator.StringToHash("Xmove");
    private readonly int YMOVE = Animator.StringToHash("Ymove");
    private readonly int DanceTrigger = Animator.StringToHash("DanceTrigger");
    private readonly int DanceChange = Animator.StringToHash("DanceIndex");
    private readonly int JumpTrigger = Animator.StringToHash("Jump");
    private readonly int IsGroundedBool = Animator.StringToHash("IsGrounded");

    private int danceIndex = 0;
    private bool isSprinting;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerInputs = GetComponent<PlayerInputs>();
        playerMovement = GetComponent<PlayerMovementController>();
    }

    private void OnEnable()
    {
        playerInputs.OnSprint += val => isSprinting = val;

        playerInputs.OnDanceTriggered += val =>
        {
            StartCoroutine(PlayEmote());
        };

        playerInputs.OnDanceChanged += _ => animator.SetFloat(DanceChange, (++danceIndex) % 2);
        playerInputs.OnJump += val =>
        {
            if (playerMovement.IsGrounded)
                animator.SetTrigger(JumpTrigger);
        };
    }

    private IEnumerator PlayEmote()
    {
        animator.SetTrigger(DanceTrigger);
        AudioSource.PlayClipAtPoint(danceMusicsList[danceIndex % 2], transform.position);
        playerInputs.enabled = false;
        animator.applyRootMotion = true;
        var lookScript = GetComponent<PlayerLookForward>();
        lookScript.enabled = false;
        yield return new WaitForSeconds(.5f);
        var time = animator.GetCurrentAnimatorStateInfo(0).length;
        Debug.Log(time);
        yield return new WaitForSeconds(time);
        lookScript.enabled = true;
        animator.applyRootMotion = false;
        playerInputs.enabled = true;
    }

    private void Update()
    {
        var move = playerInputs.MoveInput;

        if (isSprinting)
            move *= 2;

        animator.SetFloat(XMOVE, Mathf.Lerp(animator.GetFloat(XMOVE), move.x, Time.deltaTime * 5));
        animator.SetFloat(YMOVE, Mathf.Lerp(animator.GetFloat(YMOVE), move.y, Time.deltaTime * 5));

        animator.SetBool(IsGroundedBool, playerMovement.IsGrounded);
        if (!playerMovement.IsGrounded)
            animator.ResetTrigger(JumpTrigger);
    }
}

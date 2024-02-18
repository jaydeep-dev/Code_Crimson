using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintSpeed = 5f;
    [SerializeField] private float jumpHeight = 1.0f;

    [SerializeField] private Transform groundCheckTransform;

    private PlayerInputs playerInputs;
    private CharacterController controller;
    private const float gravityValue = -9.81f;
    private Vector3 playerVelocity;

    private Coroutine jumpRoutine = null;
    private Transform camTransform;

    public bool IsGrounded { get; private set; }

    private void Awake()
    {
        playerInputs = GetComponent<PlayerInputs>();
        controller = GetComponent<CharacterController>();
        camTransform = Camera.main.transform;
    }

    private void FixedUpdate()
    {
        var move = new Vector3(playerInputs.MoveDir.x, 0, playerInputs.MoveDir.y);
        var finalMove = move.x * camTransform.right + move.z * camTransform.forward;
        var finalMoveSpeed = playerInputs.IsSprinting ? sprintSpeed : moveSpeed;
        controller.Move(Time.fixedDeltaTime * finalMoveSpeed * finalMove);

        IsGrounded = Physics.Raycast(groundCheckTransform.position, Vector3.down, .5f);

        if (IsGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // Changes the height position of the player..
        if (playerInputs.IsJumped && IsGrounded)
        {
            //if(jumpRoutine == null)
            {
                jumpRoutine = StartCoroutine(DelayedJump());
            }
        }

        playerVelocity.y += gravityValue * Time.fixedDeltaTime;
        controller.Move(playerVelocity * Time.fixedDeltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(groundCheckTransform.position, .5f);
    }

    private IEnumerator DelayedJump()
    {
        yield return new WaitForSeconds(.6f);
        playerVelocity.y += Mathf.Sqrt(jumpHeight * -1.0f * gravityValue);
        jumpRoutine = null;
    }
}
using System;
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
    public bool IsSprinting { get; private set; }

    private void Awake()
    {
        playerInputs = GetComponent<PlayerInputs>();
        controller = GetComponent<CharacterController>();
        camTransform = Camera.main.transform;
    }

    private void OnEnable()
    {
        playerInputs.OnJump += OnJump;
        playerInputs.OnSprint += val => IsSprinting = val;
    }

    private void OnJump(bool obj)
    {
        if (IsGrounded && jumpRoutine == null)
        {
            Debug.Log("JUMP");
            jumpRoutine = StartCoroutine(DelayedJump());
        }
    }

    private void Update()
    {
        IsGrounded = Physics.Raycast(groundCheckTransform.position, Vector3.down, .5f);
    }

    private void FixedUpdate()
    {
        var moveInput = playerInputs.MoveInput;
        var move = new Vector3(moveInput.x, 0, moveInput.y);
        move.Normalize();

        var finalMove = move.x * camTransform.right + move.z * camTransform.forward;
        var finalMoveSpeed = IsSprinting ? sprintSpeed : moveSpeed;

        controller.Move(Time.fixedDeltaTime * finalMoveSpeed * finalMove);


        if (IsGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // Changes the height position of the player..
        playerVelocity.y += gravityValue * Time.fixedDeltaTime;
        controller.Move(playerVelocity * Time.fixedDeltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(groundCheckTransform.position, .5f);
    }

    private IEnumerator DelayedJump()
    {
        yield return new WaitForSeconds(.24f);
        playerVelocity.y += Mathf.Sqrt(jumpHeight * -1.0f * gravityValue);
        yield return new WaitForSeconds(.24f);
        jumpRoutine = null;
    }
}
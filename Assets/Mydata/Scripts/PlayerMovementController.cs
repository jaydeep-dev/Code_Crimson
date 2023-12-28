using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintSpeed = 5f;

    private PlayerInputs playerInputs;
    private CharacterController controller;
    private const float gravityValue = -9.81f;
    private const float jumpHeight = 1.0f;
    private bool groundedPlayer;
    private Vector3 playerVelocity;

    private void Awake()
    {
        playerInputs = GetComponent<PlayerInputs>();
        controller = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        var move = new Vector3(playerInputs.MoveDir.x, 0, playerInputs.MoveDir.y);
        move.Normalize();

        var finalSpeed = playerInputs.IsSprinting ? moveSpeed + sprintSpeed : moveSpeed;
        controller.Move(Time.fixedDeltaTime * finalSpeed * move);

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Debug.Log(playerInputs.IsJumped && controller.isGrounded);

        // Changes the height position of the player..
        if (playerInputs.IsJumped && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.fixedDeltaTime;
        controller.Move(playerVelocity * Time.fixedDeltaTime);
    }

}

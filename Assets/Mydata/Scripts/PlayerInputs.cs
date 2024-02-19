using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{

    public Vector2 MoveInput {  get; private set; }
    public Vector2 LookInput { get; private set; }
    public event Action<bool> OnJump;
    public event Action<bool> OnFire;
    public event Action<bool> OnSprint;
    public event Action<bool> OnDanceTriggered;
    public event Action<bool> OnDanceChanged;

    private PlayerActionMap playerActionMap;

    private void Awake()
    {
        playerActionMap = new PlayerActionMap();
    }

    private void OnEnable()
    {
        playerActionMap.Player.Enable();

        SubscribeToInputEvents();
    }

    private void OnDisable()
    {
        playerActionMap.Player.Disable();
    }

    private void SubscribeToInputEvents()
    {
        // Movement
        playerActionMap.Player.Move.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
        playerActionMap.Player.Move.canceled += ctx => MoveInput = Vector2.zero;

        // Look
        playerActionMap.Player.Look.performed += ctx => LookInput = ctx.ReadValue<Vector2>();
        playerActionMap.Player.Look.performed += ctx => LookInput = Vector2.zero;

        // Jump
        playerActionMap.Player.Jump.performed += ctx => OnJump?.Invoke(true);
        //playerActionMap.Player.Jump.canceled += ctx => OnJump?.Invoke(false);

        // Fire
        playerActionMap.Player.Fire.performed += ctx => OnFire?.Invoke(true);
        playerActionMap.Player.Fire.canceled += ctx => OnFire?.Invoke(false);

        // Sprint
        playerActionMap.Player.Sprint.performed += ctx => OnSprint?.Invoke(true);
        playerActionMap.Player.Sprint.canceled += ctx => OnSprint?.Invoke(false);

        // Dance (Trigger)
        playerActionMap.Player.DanceTrigger.performed += ctx => OnDanceTriggered?.Invoke(true);
        //playerActionMap.Player.DanceTrigger.canceled += ctx => OnDanceTriggered?.Invoke(false);

        // Dance (Change)
        playerActionMap.Player.DanceChange.performed += ctx => OnDanceChanged?.Invoke(true);
        //playerActionMap.Player.DanceChange.canceled += ctx => OnDanceChanged?.Invoke(false);
    }

    private void Update()
    {
        //MoveDir = playerActionMap.Player.Move.ReadValue<Vector2>();
        //LookDir = playerActionMap.Player.Look.ReadValue<Vector2>();

        //IsFiring = playerActionMap.Player.Fire.IsPressed();
        //IsJumped = playerActionMap.Player.Jump.WasPressedThisFrame();
        //IsSprinting = playerActionMap.Player.Sprint.IsPressed();
        //IsDanceTriggered = playerActionMap.Player.DanceTrigger.WasPressedThisFrame();
        //IsDanceChangeRequested = playerActionMap.Player.DanceChange.WasPressedThisFrame();
    }
}

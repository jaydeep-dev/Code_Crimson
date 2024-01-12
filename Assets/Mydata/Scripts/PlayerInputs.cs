using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{

    private PlayerActionMap playerActionMap;

    public bool IsFiring { get; private set; }

    public bool IsJumped { get; private set; }

    public bool IsSprinting { get; private set; }

    public Vector2 LookDir {  get; private set; }

    public Vector2 MoveDir { get; private set; }

    public bool IsDanceTriggered { get; private set; }

    public bool IsDanceChangeRequested { get; private set; }

    private void Awake()
    {
        playerActionMap = new PlayerActionMap();
    }

    private void OnEnable()
    {
        playerActionMap.Enable();
        playerActionMap.Player.Enable();
    }

    private void OnDisable()
    {
        playerActionMap.Disable();
        playerActionMap.Player.Disable();
    }

    private void Update()
    {
        MoveDir = playerActionMap.Player.Move.ReadValue<Vector2>();
        LookDir = playerActionMap.Player.Look.ReadValue<Vector2>();

        IsFiring = playerActionMap.Player.Fire.triggered;
        IsJumped = playerActionMap.Player.Jump.WasPressedThisFrame();
        IsSprinting = playerActionMap.Player.Sprint.IsPressed();
        IsDanceTriggered = playerActionMap.Player.DanceTrigger.WasPressedThisFrame();
        IsDanceChangeRequested = playerActionMap.Player.DanceChange.WasPressedThisFrame();
    }
}

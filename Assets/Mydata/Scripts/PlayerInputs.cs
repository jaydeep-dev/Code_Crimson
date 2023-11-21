using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    public bool IsFiring { get; private set; }

    public Vector2 LookDir {  get; private set; }

    public void OnFire(InputAction.CallbackContext value)
    {
        IsFiring = value.action.triggered;
        Debug.Log(IsFiring);
    }

    public void OnLook(InputAction.CallbackContext value)
    {
        LookDir = value.ReadValue<Vector2>();
    }
}

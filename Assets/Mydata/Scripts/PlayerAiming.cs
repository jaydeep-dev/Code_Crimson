using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAiming : MonoBehaviour
{
    private RaycastWeapon weapon;
    private PlayerInputs playerInputs;

    private void Awake()
    {
        playerInputs = GetComponent<PlayerInputs>();
        weapon = GetComponentInChildren<RaycastWeapon>();
    }

    private void Update()
    {
        if(playerInputs.IsFiring)
            weapon.StartFiring();
        else
            weapon.StopFiring();
    }
}

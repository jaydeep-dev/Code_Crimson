using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAiming : MonoBehaviour
{
    private bool isFiring;
    private RaycastWeapon weapon;
    private PlayerInputs playerInputs;

    private void Awake()
    {
        playerInputs = GetComponent<PlayerInputs>();
        weapon = GetComponentInChildren<RaycastWeapon>();
    }

    private void OnEnable()
    {
        playerInputs.OnFire += val => isFiring = val;
    }

    private void Update()
    {
        if(isFiring)
            weapon.StartFiring();
        else
            weapon.StopFiring();
    }
}

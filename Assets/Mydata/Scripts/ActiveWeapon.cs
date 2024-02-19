using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] private Transform weaponParent;
    [SerializeField] private Transform crosshairTarget;
    [SerializeField] private Rig handIK;

    private bool isFiring;
    private RaycastWeapon weapon;
    private PlayerInputs playerInputs;

    private void Awake()
    {
        playerInputs = GetComponent<PlayerInputs>();
        weapon = GetComponentInChildren<RaycastWeapon>();
        EquipWeapon(weapon);
    }

    private void OnEnable()
    {
        playerInputs.OnFire += val => isFiring = val;
    }

    private void Update()
    {
        if (!weapon)
            return;

        if(isFiring)
            weapon.StartFiring();
        else
            weapon.StopFiring();
    }

    public void EquipWeapon(RaycastWeapon newWeapon)
    {
        if(newWeapon == null)
        {
            handIK.weight = 0f;
            return;
        }

        if(weapon)
        {
            Destroy(weapon.gameObject);
        }

        weapon = newWeapon;
        weapon.raycastTarget = crosshairTarget;
        weapon.transform.SetParent(weaponParent);
        weapon.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

        handIK.weight = 1f;
    }
}

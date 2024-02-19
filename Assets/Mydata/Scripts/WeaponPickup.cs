using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private RaycastWeapon weaponPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out ActiveWeapon playerAiming))
        {
            var weapon = Instantiate(weaponPrefab);
            playerAiming.EquipWeapon(weapon);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastWeapon : MonoBehaviour
{
    public bool isFiring = false;
    public Transform raycastOrigin;
    public Transform raycastTarget;
    public TrailRenderer tracerEffect;
    [SerializeField] private float fireRate;

    private float currentTime;

    private Ray ray;
    private RaycastHit hitinfo;

    private void Update()
    {
        if (!isFiring)
            return;

        currentTime += Time.deltaTime;
        if (currentTime > 1f / fireRate)
        {
            currentTime = 0;
            ray.origin = raycastOrigin.position;
            ray.direction = raycastTarget.position - raycastOrigin.position;

            var tracer = Instantiate(tracerEffect, ray.origin, Quaternion.identity);
            tracer.AddPosition(ray.origin);

            if (Physics.Raycast(ray, out hitinfo))
            {
                tracer.transform.position = hitinfo.point;
            }
        }
    }

    public void StartFiring()
    {
        isFiring = true;
    }

    public void StopFiring()
    {
        isFiring= false;
    }
}

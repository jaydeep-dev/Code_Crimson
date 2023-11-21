using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastWeapon : MonoBehaviour
{
    public bool isFiring = false;
    public Transform raycastOrigin;
    public Transform raycastTarget;
    public TrailRenderer tracerEffect;

    Ray ray;
    RaycastHit hitinfo;

    public void StartFiring()
    {
        isFiring = true;

        ray.origin = raycastOrigin.position;
        ray.direction = raycastTarget.position - raycastOrigin.position;

        var tracer = Instantiate(tracerEffect, ray.origin, Quaternion.identity);
        tracer.AddPosition(ray.origin);

        if (Physics.Raycast(ray, out hitinfo))
        {
            tracer.transform.position = hitinfo.point;
        }
    }

    public void StopFiring()
    {
        isFiring= false;
    }
}

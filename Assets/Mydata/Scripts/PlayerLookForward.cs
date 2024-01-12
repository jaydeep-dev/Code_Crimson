using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookForward : MonoBehaviour
{
    Camera mainCam;

    public float turnSpeed = 15f;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void LateUpdate()
    {
        float yRot = mainCam.transform.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yRot, 0), turnSpeed * Time.deltaTime);
    }
}

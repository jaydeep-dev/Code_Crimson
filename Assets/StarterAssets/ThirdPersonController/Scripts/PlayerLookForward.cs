using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookForward : MonoBehaviour
{
    public Transform cameraTrans;


    private void LateUpdate()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        forward.y = 0f;
        Vector3 right = transform.TransformDirection(Vector3.right);
        Vector2 input = new Vector2
        {
            x = Input.GetAxis("Horizontal"),
            y = Input.GetAxis("Vertical")
        };
        Vector3 targetDir = input.x * right + Mathf.Abs(input.y) * forward;
        var lookDir = Quaternion.LookRotation(targetDir.normalized, transform.up);
        var diffRot = lookDir.y - transform.eulerAngles.y;
        var eulerY = transform.eulerAngles.y;
        if(diffRot != 0) eulerY = lookDir.y;
        var finalEuler = new Vector3(0, eulerY, 0);
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(finalEuler), Time.deltaTime * 20);
    }
}

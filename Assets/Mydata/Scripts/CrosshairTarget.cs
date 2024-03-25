using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairTarget : MonoBehaviour
{
    private Camera camera;
    Ray ray;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        ray.origin = camera.transform.position;
        ray.direction = camera.transform.forward;

        Physics.Raycast(ray, out hit);
        transform.position = hit.point;
    }
}

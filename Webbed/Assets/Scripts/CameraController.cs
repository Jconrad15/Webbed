using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(transform.position.x, transform.position.y, cam.transform.position.z);

        cam.transform.position = newPos;
    }
}

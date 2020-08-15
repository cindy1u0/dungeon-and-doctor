using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public Transform target;
    public float speed;
    public float mouseSensitivity = 100f;

    Vector3 offset;
    float temp = 0;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        // transform.RotateAround(target.position, Vector3.up, speed * Time.deltaTime);

        transform.position = target.position + offset;

        float moveX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        // float moveY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        temp += moveX;

        transform.localEulerAngles = new Vector3(45, 90 + temp, target.rotation.z);
    }
}

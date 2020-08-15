using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    Transform playerBody;
    float pitch = 0;

    void Start()
    {
        playerBody = transform.parent.transform;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (!PauseMenuBehavior.isGamePaused)
        {
            if (!LevelManager.isGameOver)
            {

                float moveX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
                float moveY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

                // yaw - parent
                playerBody.Rotate(Vector3.up * moveX);

                // pitch - camera
                pitch -= moveY;

                pitch = Mathf.Clamp(pitch, -15f, 20f);

                transform.localRotation = Quaternion.Euler(pitch, 0, 0);
            }
        }
    }
}

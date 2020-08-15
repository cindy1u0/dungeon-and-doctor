using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorController : MonoBehaviour
{
    public float speed = 5f;
    public float jump = 1;
    public float airControl = 1;
    public float gravity = 9.81f;

    CharacterController controller;
    Vector3 input, moveDir;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!LevelManager.isGameOver)
        {
            float moveHori = Input.GetAxis("Horizontal");
            float moveVert = Input.GetAxis("Vertical");
            input = (transform.right * moveHori + transform.forward * moveVert).normalized;
            input *= speed;

            if (controller.isGrounded)
            {
                moveDir = input;

                // we can jump
                if (Input.GetButton("Jump"))
                {
                    moveDir.y = Mathf.Sqrt(2 * jump * gravity);
                }
                else
                {
                    moveDir.y = 0.0f;
                }

            }
            else
            {
                // midair
                input.y = moveDir.y;
                moveDir = Vector3.Lerp(moveDir, input, airControl * Time.deltaTime);

            }

            moveDir.y -= gravity * Time.deltaTime;
            controller.Move(moveDir * Time.deltaTime);
        }
    }
}

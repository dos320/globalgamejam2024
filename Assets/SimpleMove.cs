using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class SimpleMove : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float fPlayerSpeed = 2.0f;
    private float fJumpHeight = 1.0f;
    private float fGravityValue = -9.81f;
    private float fVelocityY = 0;
    public float fSpeed;
    public Camera camera;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        fSpeed = 10;
    }

    // TODO: fix the jumping
    void Update()
    {

        Vector3 input = Quaternion.Euler(0, controller.transform.eulerAngles.y, 0) * new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        //input = input.normalized;

        //reading the input:
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        //assuming we only using the single camera:
        var camera = Camera.main;

        //camera forward and right vectors:
        var forward = camera.transform.forward;
        var right = camera.transform.right;

        //project forward and right vectors on the horizontal plane (y = 0)
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        //this is the direction in the world space we want to move:
        var desiredMoveDirection = forward * verticalAxis + right * horizontalAxis;

        //now we can apply the movement:
        //transform.Translate(desiredMoveDirection * fSpeed * Time.deltaTime);

        Vector3 velocity = input * fSpeed;
        fVelocityY += fGravityValue * Time.deltaTime;
        velocity.y = fVelocityY;

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(fJumpHeight * -3.0f * fGravityValue);
        }


        controller.Move(velocity * Time.deltaTime);
        //controller.Move(desiredMoveDirection * fSpeed * Time.deltaTime);
        //transform.position = transform.position + Camera.main.transform.forward * fSpeed * Time.deltaTime;
        //transform.position += velocity * Time.deltaTime;
      

        if (controller.isGrounded)
        {
            fVelocityY = 0;
        }

        
    }

}

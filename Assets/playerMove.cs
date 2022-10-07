using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    public CharacterController controller;
    public float speed;
    public float gravity;
    public Vector3 velocity;
    public float jumpVelocity;

    public Transform groundCheck;
    public float groundDistance = 0.5f;
    public LayerMask groundMask;
    public bool isGrounded;
    public hookShooting hs;
    public AudioSource jumpSound;

    void Start()
    {
        jumpVelocity = Mathf.Sqrt(3f * -2 * gravity);
        isGrounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isGrounded && velocity.y < 0) velocity.y = 0;
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        int x = 0, z = 0;
        if (Input.GetKey("w")) z++;
        if (Input.GetKey("s")) z--;
        if (Input.GetKey("d")) x++;
        if (Input.GetKey("a")) x--;

        Vector3 move = x * transform.right + z * transform.forward;

        if (!isGrounded)
        {
            move *= 0.95f;
            if (velocity.magnitude > 10f / 60f) velocity -= velocity.normalized * 10f * Time.smoothDeltaTime;
            else velocity = Vector3.zero;
        }
        else if (move.x == 0 && move.y == 0 && !hs.pullPlayer) velocity -= velocity.normalized * 100f * Time.smoothDeltaTime;


        controller.Move(move * Time.smoothDeltaTime * speed);

        if (Input.GetKey("space") && isGrounded && !hs.pullPlayer)
        {
            velocity.y = jumpVelocity;
            jumpSound.Play();
        }

        velocity.y += gravity * Time.smoothDeltaTime;

        controller.Move(velocity * Time.smoothDeltaTime);
    }
}

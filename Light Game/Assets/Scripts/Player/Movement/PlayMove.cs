using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMove : MonoBehaviour
{
    //controller object reference
    public CharacterController controller;
    public ParticleSystem poof;

    //forces acting on object
    public float gravity = -9.81f;
    private float normalSpeed;
    private float fastSpeed;
    private float slowSpeed;
    public float speed = 12f;
    public float jumpHeight = 3f;
    private float curStam;
    public float maxStam = 10f;
    private bool isSprinting = false;

    //objects and values for checking ground referance
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    //values for checking controller is grounded
    Vector3 velocity;
    bool isGrounded;

    void Start()
    {
        curStam = maxStam;
        normalSpeed = speed;
        fastSpeed = normalSpeed + (normalSpeed / 2);
        slowSpeed = normalSpeed - (normalSpeed / 2);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.LeftShift) && curStam >= 0f)
        {
            isSprinting = true;
            curStam -= 1 * Time.deltaTime;
        }
        else
        {
            isSprinting = false;
            /*TODO: need to implement wait time for curStam < 0 without altering movement
                    may involve a seperate "sprint" script */
            if (curStam < maxStam)
            {
                curStam += (1 * Time.deltaTime) / 2;
                if (curStam > maxStam)
                {
                    curStam = maxStam;
                }
            }
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            speed = slowSpeed;
            this.transform.GetChild(0).transform.localPosition = new Vector3(0f, 0f, 0f);
        }
        else
        {
            if (isSprinting)
            {
                poof.Play();
                speed = fastSpeed;
            }
            else
            {
                speed = normalSpeed;
            }
            this.transform.GetChild(0).transform.localPosition = new Vector3(0f, .6f, 0f);
        }
        //checks with if our floorcheck G.O is inside of any object labelled under groundmask
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //check if player is grounded and adjusts downwards velocity
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //get horizontal and vertical keys as input
        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");

        //applys left and right force to player object

        Vector3 move = transform.right * xMove + transform.forward * zMove;
        controller.Move(move * speed * Time.deltaTime);

        //adds upward force if spacebar is pressed and player is on the ground
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y += Mathf.Sqrt(jumpHeight * gravity * -2f);
            controller.Move(move * speed * Time.deltaTime);
        }

        //make upward force and apply force to player object
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        Debug.Log((int)curStam + ", " + maxStam + "| " + speed);
    }
}
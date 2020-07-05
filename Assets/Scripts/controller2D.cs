using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimationManager))]
public class controller2D : MonoBehaviour
{
    //Variables
    public float Speed = 5;
    public float minSpeed = 1;
    private float moveInput;
    private bool canMove = true;

    //if character can be controlled manually with keyboard on false it will go forward on it's own
    public bool manualControlling = true;

    public float JumpForce = 17;

    private Rigidbody2D rb;

    //sprite variables
    bool facingRight = true;
    float lastPosX;
    Vector3 startScale;
    Vector3 currentScale;

    //ground check
    [SerializeField]
    bool isGrounded;
    public Transform GroundCheck;
    public float CheckRadius;
    public LayerMask WhatIsGround;

    //animations
    PlayerAnimationManager aniMan;

    public Rigidbody2D Rb { get => rb; set => rb = value; }
    public bool CanMove { get => canMove; set => canMove = value; }

    void Start()
    {
        //rigid body
        Rb = GetComponent<Rigidbody2D>();

        //animation manager
        aniMan = GetComponent<PlayerAnimationManager>();

        //object's scale at the start
        currentScale = startScale = transform.localScale;

        lastPosX = transform.position.x;
        CanMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        #region don't worry about this
        if (Input.GetButtonDown("Fire2"))
        {
            ScreenCapture.CaptureScreenshot(DateTime.Now.ToString());
        }
        #endregion

        if (CanMove)
        {
            //change the direction the sprite is facing
            if (lastPosX < transform.position.x && !facingRight)//moves right but isn't facing right
            {
                facingRight = true;
                flipSprite();
            }
            if (lastPosX > transform.position.x && facingRight)//moves left but isn't facing left
            {
                facingRight = false;
                flipSprite();
            }

            //for manual controlling
            if (!manualControlling)
            {
                moveInput = .5f;
            }
            else
            {
                moveInput = Input.GetAxis("Horizontal");
            }

            //animations in animationmanager script
            aniMan.Speed = Math.Abs(Rb.velocity.x);
            //aniMan.IsInAir = !isGrounded;

            //has to be last in update
            lastPosX = transform.position.x;
        }
    }

    void FixedUpdate()
    {
        //cheaper to use this here also helps with sliding controls
        Rb.velocity = new Vector2(moveInput * Speed, Rb.velocity.y);

        //ground checks
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, CheckRadius, WhatIsGround);

        if (canMove)
        {
            //jumping
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                //Debug.Log("jumping");
                AudioManager.instance.PlaySound("Jump");
                Rb.velocity = Vector3.up * JumpForce;
            }
        }
    }

    //scale the object so that it's facing the other direction
    public void flipSprite()
    {
        if (!facingRight)
        {
            transform.localScale = new Vector3(-currentScale.x, currentScale.y, currentScale.z);
        }
        else
        {
            transform.localScale = new Vector3(currentScale.x, currentScale.y, currentScale.z);
        }
    }

    public void Grow(float amount)
    {
        transform.localScale = new Vector3(transform.localScale.x + amount, transform.localScale.y + amount, 0);
        if (transform.localScale.x > 3 || transform.localScale.y > 3)
        {
            transform.localScale = new Vector3(3, 3, 0);
        }
        if (transform.localScale.x < 1 || transform.localScale.y < 1)
        {
            transform.localScale = new Vector3(1, 1, 0);
        }
        currentScale = transform.localScale;
    }

    //speed changing
    public void IncreaseSpeed(float amount)
    {
        Speed += amount;
    }
    public void DecreaseSpeed(float amount)
    {
        Speed -= amount;

        if (Speed < minSpeed)
        {
            Speed = minSpeed;
        }
    }
}

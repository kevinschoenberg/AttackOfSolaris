using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float accSpeed = 800;
    public float maxSpeed = 10;

    public float jumpForce = 20;
    private float _inputX;


    public bool isFacingRight = true;
    
    public AudioSource jumpSound;
    
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    
    public GameObject planet;
    //Used for animation
    public Transform CenterPoint;
    public float PlayerAngle;
    public float PlayerAngleOld;
    private SpriteRenderer sprite;
    private Animator anim;
    private Animator animFire;
    private enum MovementState {idle, running, jumping, falling};

    //Jetpack
    public float maxFuel = 10f;
    public float fuel = 10f;
    public float JetpackForce = 20;
    public FuelBar fuelBar;
    public bool hasJetpack = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        CenterPoint = GameObject.Find("CenterPoint").GetComponent<Transform>();
        if(hasJetpack)
        {
            animFire = GameObject.Find("Fire").GetComponent<Animator>();
            if (fuelBar != null)
                fuelBar.SetMaxFuel(maxFuel);
            fuel = maxFuel;
        }
    }

    void Update()
    {
        
        //Debug.Log(transform.InverseTransformDirection(rb.velocity));
        _inputX = Input.GetAxis("Horizontal");
        if (_inputX > 0f)
        {
            rb.AddRelativeForce(Vector2.right * (Time.deltaTime * accSpeed));
            //transform.Translate(Vector2.right * (Time.deltaTime * speed), Space.Self);
        }
        else if (_inputX < 0f)
        {
            rb.AddRelativeForce(Vector2.left * (Time.deltaTime * accSpeed));
            //transform.Translate(Vector2.left * (Time.deltaTime * speed), Space.Self);
        }
        else
        {
            rb.angularVelocity = 0f;
            
            if(IsGrounded())
                rb.velocity = Vector2.zero;
        }
        rb.velocity = transform.TransformDirection(Vector2.ClampMagnitude(transform.InverseTransformDirection(rb.velocity), maxSpeed));
        
        if(Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpSound.Play();
            Vector3 v = transform.position - planet.transform.position;
            rb.AddForce(v * jumpForce);
        }
        if(Input.GetKey(KeyCode.F) && fuel > 0 && hasJetpack)
        {
            float time_passed = Time.deltaTime;
            float dist = Vector3.Distance(Vector3.zero, transform.position);
            float dist_mult = 205.025f/dist;
            Vector3 v = transform.position - planet.transform.position;
            rb.AddForce(v*JetpackForce*dist_mult);
            fuel -= time_passed;
            animFire.SetTrigger("JetPackOn");
            if (fuelBar != null)
                fuelBar.SetFuel(fuel);
        }
        else if (hasJetpack)
        { 
            animFire.ResetTrigger("JetPackOn");
            if (fuelBar != null)
                fuelBar.SetFuel(fuel);
        }
        UpdateAnimationState();
        Flip();
    }
    
    private void UpdateAnimationState()
    {
        MovementState State;
        State = MovementState.idle;
        Vector2 CenterToPlayer = transform.position - CenterPoint.transform.position;
        PlayerAngle = Mathf.Atan2(CenterToPlayer.x, CenterToPlayer.y) * Mathf.Rad2Deg * Mathf.Sign(CenterToPlayer.x) - 90;
        if (IsGrounded())
        {
            if (_inputX > 0f)
            {
                State = MovementState.running;
                sprite.flipX = false;
            }
            else if (_inputX < 0f)
            {
                State = MovementState.running;
                sprite.flipX = true;
            }
            else
            {
                State = MovementState.idle;
            }
        }            
        else
        {

            if (PlayerAngle > 0 && rb.velocity.y > 0f)
            {
                State = MovementState.falling;
            }
            else if (PlayerAngle > 0 && rb.velocity.y < 0f)
            {
                State = MovementState.jumping;
            }
            else if (PlayerAngle < 0 && rb.velocity.y < 0f)
            {
                State = MovementState.falling;
            }
            else if (PlayerAngle < 0 && rb.velocity.y > 0f)            
            {
                State = MovementState.jumping;
            }   
        }
        anim.SetInteger("AnimState", (int)State);
    }

    public bool IsGrounded()
    { 
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    
    private void Flip()
    {
        if(isFacingRight && _inputX < 0f || !isFacingRight && _inputX > 0f)
        {
            isFacingRight = !isFacingRight;
        }
    }
    
}



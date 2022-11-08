using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 5;

    public float jumpForce = 20;
    private float _inputX;

    private SpriteRenderer sprite;
    private Animator anim;
    private enum MovementState {idle, running, jumping, falling};
    public bool isFacingRight = true;
    
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    
    public GameObject planet;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        _inputX = Input.GetAxis("Horizontal");
        if (_inputX > 0f)
        {
            transform.Translate(Vector2.right * (Time.deltaTime * speed), Space.Self);
        }
        else if (_inputX < 0f)
        {
            transform.Translate(Vector2.left * (Time.deltaTime * speed), Space.Self);
        }
        else
        {
            rb.angularVelocity = 0f;
            
            if(IsGrounded())
                rb.velocity = Vector2.zero;
                
        }
        if(Input.GetButtonDown("Jump") && IsGrounded())
        {
            Vector3 v = transform.position - planet.transform.position;
            rb.AddForce(v * jumpForce);
        }
        UpdateAnimationState();
    }
    
    private void UpdateAnimationState()
    {
        MovementState State;
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
        if(rb.velocity.y > .1f)
        {
            State = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            State = MovementState.falling;
        }
        anim.SetInteger("AnimState", (int)State);
    }

    public bool IsGrounded()
    { 
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    /*
    private void Flip()
    {
        if(isFacingRight && _inputX < 0f || !isFacingRight && _inputX > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    */
}



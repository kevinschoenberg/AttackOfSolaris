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
            anim.SetBool("running", true);
            sprite.flipX = false;
            transform.Translate(Vector2.right * (Time.deltaTime * speed), Space.Self);
        }
        else if (_inputX < 0f)
        {
            anim.SetBool("running", true);
            sprite.flipX = true;
            transform.Translate(Vector2.left * (Time.deltaTime * speed), Space.Self);
        }
        else
        {
            anim.SetBool("running", false);
            transform.Translate(Vector2.zero, Space.Self);
        }
        if(Input.GetButtonDown("Jump") && IsGrounded())
        {
            Vector3 v = transform.position - planet.transform.position;
            rb.AddForce(v * jumpForce);
        }

        //Flip();
    }

    private bool IsGrounded()
    { 
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
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
}



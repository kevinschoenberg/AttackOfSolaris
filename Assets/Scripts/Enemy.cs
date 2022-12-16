using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class Enemy : MonoBehaviour
{   
    private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    /*Variables for animation*/
    public Rigidbody2D rb;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheck = transform.Find("GroundCheck");
    }
    // Update is called once per frame
    void Update()
    {
        //dirX = rb.velocity.x;
        //UpdateAnimationState();
        if(!groundCheck.IsUnityNull() && (IsGrounded()))
            rb.velocity = Vector2.zero;     
    }

    public bool IsGrounded()
    { 
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }
}

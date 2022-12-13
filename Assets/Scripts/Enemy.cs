using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{   
    [SerializeField] public GameObject player;



    /*Variables for animation*/
    public Rigidbody2D rb;
    public float dirX;
    private enum MovementState {idle, walking};
    private SpriteRenderer sprite;
    private Animator anim;



    public static int MaxDist = 7;
    public static int MinDist = 2;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        //dirX = rb.velocity.x;
        //UpdateAnimationState();        
    }

}

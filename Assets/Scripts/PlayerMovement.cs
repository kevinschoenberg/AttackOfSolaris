using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float accSpeed = 800;
    public float maxSpeed = 10f;

    public float jumpForce = 20;
    private float _inputX;
    private float _timeSinceJump;


    public bool isFacingRight = true;
    
    public AudioSource jumpSound;
    public AudioSource JetPackSound;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask lavaLayer;
    
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

    public bool hasBothMeleeAndRange = false;

    //Swap between melee and range
    Shooting shooting;
    Melee melee;
    GameObject weopen;
    GameObject gunRotationPoint;
    SpriteRenderer UISword;
    SpriteRenderer UIAK47;
    SpriteRenderer UIArrow;
    GameObject q;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        AdjustVolume AdjVol = GameObject.Find("Sound").transform.Find("Volume").gameObject.GetComponent<AdjustVolume>();
        JetPackSound.volume = AdjVol.GetVolume()/2;
        CenterPoint = GameObject.Find("CenterPoint").GetComponent<Transform>();
        if(fuelBar != null)
        {
            animFire = GameObject.Find("Fire").GetComponent<Animator>();
            fuelBar.SetMaxFuel(maxFuel);
            fuel = maxFuel;
        }
            UISword = transform.Find("Canvas/UI_Sword").GetComponent<SpriteRenderer>();
            UIAK47 = transform.Find("Canvas/UI_AK47").GetComponent<SpriteRenderer>();
            UIArrow = transform.Find("Canvas/UI_Arrow").GetComponent<SpriteRenderer>();
            melee = GetComponent<Melee>();
            shooting = GetComponent<Shooting>();
            weopen = transform.Find("Weopen/Sword").gameObject;
            gunRotationPoint = transform.Find("GunRotationPoint").gameObject;

            if (melee.enabled)
            {
                UISword.color = new Color(1f,1f,1f,1f);
                UIAK47.color = new Color(1f,1f,1f,.5f);
            }
            else
            {
                UISword.color = new Color(1f,1f,1f,.5f);
                UIAK47.color = new Color(1f,1f,1f,1f);
            }
        if (!hasBothMeleeAndRange)
        {
            q = GameObject.Find("StageManager/TextBoxes/Q");
            UISword = transform.Find("Canvas/UI_Sword").GetComponent<SpriteRenderer>();
            UIAK47 = transform.Find("Canvas/UI_AK47").GetComponent<SpriteRenderer>();
            UIArrow = transform.Find("Canvas/UI_Arrow").GetComponent<SpriteRenderer>();
            q.SetActive(false);
            UISword.color = new Color(1f,1f,1f,0f);
            UIAK47.color = new Color(1f,1f,1f,0f);
            UIArrow.color = new Color(1f,1f,1f,0f);
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
            
            if(IsGrounded() && Time.time > _timeSinceJump + .1f)
                rb.velocity = Vector2.zero;
        }

        var relativeVel = transform.InverseTransformDirection(rb.velocity);
        var limitedX = math.clamp(relativeVel.x, -maxSpeed, maxSpeed);
        var limitedY = math.clamp(relativeVel.y, -500f, 10f);
        rb.velocity = transform.TransformDirection(new Vector3(limitedX, limitedY, relativeVel.y));
        //rb.velocity = transform.TransformDirection(Vector2.ClampMagnitude(transform.InverseTransformDirection(rb.velocity), maxSpeed));
        
        if(Input.GetButtonDown("Jump") && (IsGrounded() || OnLava()))
        {
            jumpSound.Play();
            Vector3 v = transform.position - planet.transform.position;
            rb.AddForce(v * jumpForce);
            _timeSinceJump = Time.time;
        }
        if(Input.GetKey(KeyCode.F) && fuel > 0 && hasJetpack)
        {
            float time_passed = Time.deltaTime;
            float dist = Vector3.Distance(Vector3.zero, transform.position);
            float dist_mult = 205.025f/dist;
            Vector3 v = transform.position - planet.transform.position;
            rb.AddForce(v*JetpackForce*dist_mult*(50*time_passed));
            fuel -= time_passed;
            animFire.SetTrigger("JetPackOn");
            fuelBar.SetFuel(fuel);
            if (JetPackSound.isPlaying == false)
                JetPackSound.Play();
        }
        else if (hasJetpack)
        { 
            animFire.ResetTrigger("JetPackOn");
            fuelBar.SetFuel(fuel);
            if (JetPackSound.isPlaying== true)
                JetPackSound.Stop();
        }
        if(Input.GetKeyDown(KeyCode.Q) && hasBothMeleeAndRange)
        {
            swapMeleeRange();
        }
        UpdateAnimationState();
        Flip();
    }

    [ContextMenu("SwapBetweenMeleeAndRange")]
    public void swapMeleeRange()
    {
        UISword = transform.Find("Canvas/UI_Sword").GetComponent<SpriteRenderer>();
        UIAK47 = transform.Find("Canvas/UI_AK47").GetComponent<SpriteRenderer>();
        UIArrow = transform.Find("Canvas/UI_Arrow").GetComponent<SpriteRenderer>();
        melee = GetComponent<Melee>();
        shooting = GetComponent<Shooting>();
        weopen = transform.Find("Weopen/Sword").gameObject;
        gunRotationPoint = transform.Find("GunRotationPoint").gameObject;
        if (melee.enabled)
        {
            UISword.color = new Color(1f,1f,1f,.5f);
            UIAK47.color = new Color(1f,1f,1f,1f);
        }
        else
        {
            UISword.color = new Color(1f,1f,1f,1f);
            UIAK47.color = new Color(1f,1f,1f,.5f);
        }
        //Active for shooting
        gunRotationPoint.SetActive(melee.enabled);
        shooting.enabled = melee.enabled;
        //Active for Melee
        melee.enabled = !melee.enabled;
        weopen.SetActive(melee.enabled);
    }
    
    private void UpdateAnimationState()
    {
        MovementState State;
        State = MovementState.idle;
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
            if (_inputX > 0f)
            {
                sprite.flipX = false;
            }
            else if (_inputX < 0f)
            {
                sprite.flipX = true;
            }
            Vector2 CenterToPlayer = transform.position - CenterPoint.transform.position;
            PlayerAngle = Mathf.Atan2(CenterToPlayer.x, CenterToPlayer.y) * Mathf.Rad2Deg * Mathf.Sign(CenterToPlayer.x) - 90;
            if (PlayerAngle > 0)
            {
                if (rb.velocity.y > 0f)
                {
                    State = MovementState.falling;
                    
                }
                else
                {
                    State = MovementState.jumping;
                } 
            }
            else
            {
                if (rb.velocity.y < 0f)
                {
                    State = MovementState.falling;
                }
                else           
                {
                    State = MovementState.jumping;
                }  
            }
 
        }
        anim.SetInteger("AnimState", (int)State);
    }

    public bool IsGrounded()
    { 
        return Physics2D.OverlapCircle(groundCheck.position, 0.25f, groundLayer);
    }
    public bool OnLava()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, lavaLayer);
    }
    private void Flip()
    {
        if(isFacingRight && _inputX < 0f || !isFacingRight && _inputX > 0f)
        {
            isFacingRight = !isFacingRight;
        }
    }
}



using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class PatrolIgnore : MonoBehaviour
{
    [SerializeField] public float walkingTime = 5f;
    public Transform spawnPoint;
    private int index = 1;
    private float _lastTime = 0f;
    private float _currentTime = 0f;
    private float _walkingSpeed = 2f;
    //For animation
    private SpriteRenderer sprite;
    private Animator anim;
    private enum MovementState {idle, notAlert, isAlert, isIgnore};
    MovementState State;
    bool commander = false;
    public bool facingRight = true;
    public bool oldfacingRight = true;
    Vector2 spawnPointPos;

    void Start()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        commander = name.Contains("Enemy_Command");
        if (commander)
        {
            spawnPoint = transform.Find("SpawnPoint").transform;
            spawnPointPos = spawnPoint.position;
        }
        State = MovementState.isIgnore;
        anim.SetInteger("AnimState", (int)State);
    }

    private void Update()
    {
        if (index < 0)
        {
            
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
        oldfacingRight = facingRight;
        if (_lastTime == 0f)
        {
            _lastTime = Time.time;
            _currentTime = Time.time;
        }
        if (_currentTime - _lastTime < walkingTime)
        {
            transform.Translate(Vector2.left * (Time.deltaTime * _walkingSpeed * index), Space.Self);
            _currentTime = Time.time;
        }
        else
        {
            facingRight = !facingRight;
            index *= -1;
            _lastTime = Time.time;
        }
    }    
}

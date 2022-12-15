using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class PatrolIgnore : MonoBehaviour
{
    [SerializeField] public float walkingTime = 5f;

    private int index = 1;
    private float _lastTime = 0f;
    private float _currentTime = 0f;
    private float _walkingSpeed = 2f;
    //For animation
    private SpriteRenderer sprite;
    private Animator anim;
    private enum MovementState {idle, walking};
    MovementState State;

    void Start()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (index < 0)
        {
            State = MovementState.walking;
            sprite.flipX = true;
        }
        else
        {
            State = MovementState.walking;
            sprite.flipX = false;
        }
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
            index *= -1;
            _lastTime = Time.time;
        }
        anim.SetInteger("AnimState", (int)State);
    }
    

}

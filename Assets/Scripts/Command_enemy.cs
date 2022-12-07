using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command_enemy : MonoBehaviour
{
    [SerializeField] public GameObject player;
    public Transform spawnpoint;
    public GameObject enemy;


    /*Variables for animation*/
    public Rigidbody2D rb;
    public float dirX;
    private enum MovementState { idle, walking };
    private SpriteRenderer sprite;
    private Animator anim;
    private float _lastTime = 0f;
    private float spawn_offset = 2f;
    private Vector2 _distanceToPLayer;


    public static int MaxDist = 10;
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
        //if the player is within enemy.maxdist start spawning
        if (Vector2.Distance(transform.position, player.transform.position) < Enemy.MaxDist)
            if (Time.timeSinceLevelLoad > _lastTime + spawn_offset)
            {
                Spawn_enemy();
                _lastTime = Time.timeSinceLevelLoad;
            }
        //dirX = rb.velocity.x;
        //UpdateAnimationState();

    }
    public void Spawn_enemy()
    {
        Instantiate(enemy, spawnpoint.position, spawnpoint.rotation);
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command_enemy : MonoBehaviour
{
    [SerializeField] public GameObject player;
    public Transform spawnpoint;
    public GameObject enemy_to_spawn;
    Health Command_heatlh;

    /*Variables for animation*/
    public Rigidbody2D rb;
    public float dirX;
    private enum MovementState { idle, walking };
    private SpriteRenderer sprite;
    private Animator anim;
    private float _lastTime = 0f;
    private float spawn_offset = 2f;
    private Vector2 _distanceToPLayer;
    public int max_spawns = 2;
    private int spawn_count;

    


    public static int MaxDist = 10;
    public static int MinDist = 2;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        Command_heatlh = GameObject.Find("Enemy_command").GetComponent<Health>();

    }
    // Update is called once per frame
    void Update()
    {
        //if it is half health
        if(Command_heatlh.health < Command_heatlh.maxHealth/2)
            //controls the number of spaws
            if (max_spawns >= spawn_count)
            {
                //if the player is within enemy.maxdist start spawning
                if (Vector2.Distance(transform.position, player.transform.position) < Enemy.MaxDist)
                    if (Time.timeSinceLevelLoad > _lastTime + spawn_offset)
                    {
                        Spawn_enemy();
                        _lastTime = Time.timeSinceLevelLoad;
                        spawn_count++;
                    }
            }

        //dirX = rb.velocity.x;
        //UpdateAnimationState();

    }
    public void Spawn_enemy()
    {
        GameObject spawn = Instantiate(enemy_to_spawn, spawnpoint.position, spawnpoint.rotation);
        PlanetGravity pg = spawn.GetComponent<PlanetGravity>();
        pg.SetPlanet(GetComponent<PlanetGravity>().planet);

        PatrolChase pc = spawn.GetComponent<PatrolChase>();
        pc.SetPlayer(GetComponent<PatrolChase>().player);
    }

}

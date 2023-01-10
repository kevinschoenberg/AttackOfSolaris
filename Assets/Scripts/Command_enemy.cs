using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Command_enemy : MonoBehaviour
{
    [SerializeField] public GameObject player;
    public Transform spawnpointR;
    public Transform spawnpointL;
    public GameObject enemy_to_spawn_patrolChase;
    public GameObject enemy_to_spawn_idle;
    public GameObject enemy_to_spawn_gun;
    Health Command_heatlh;

    //for spawning
    public ParticleSystem spawneffect;
    public AudioClip spawnsound;



    private enum MovementState { idle, walking };
    private float _lastTime = 0f;
    private float spawn_offset = 4f;
    public int max_spawns = 2;
    private int spawn_count = 0;

    int index = 1;
    float bosSpawnHealthMult = 0.75f;
    


    public static int MaxDist = 10;
    public static int MinDist = 2;
    // Start is called before the first frame update
    void Start()
    {
        Command_heatlh = GetComponent<Health>();
    }
    // Update is called once per frame
    void Update()
    {
        //if it is half health
        if(Command_heatlh.health < (Command_heatlh.maxHealth*bosSpawnHealthMult))
            //controls the number of spaws
            if (spawn_count <= max_spawns)
            {
                //if the player is within enemy.maxdist start spawning
                if (Vector2.Distance(transform.position, player.transform.position) < MaxDist)
                    if (Time.timeSinceLevelLoad > _lastTime + spawn_offset)
                    {
                        Spawn_enemy();
                        _lastTime = Time.timeSinceLevelLoad;
                        spawn_count++;
                        print(spawn_count);
                        if (spawn_count == max_spawns)
                        {
                            spawn_count = 0;
                            bosSpawnHealthMult -= 0.25f;
                        }
                    }
            }

    }
    public void Spawn_enemy()
    {
        Transform point1 = spawnpointR;
        Transform point2 = spawnpointL;
        
        if (index > 0)
        {
            point1 = spawnpointR;
            point2 = spawnpointL;
            index *= -1;
        }
        else
        {
            point1 = spawnpointL;
            point2 = spawnpointR;
            index *= -1;
        }
        //Gun enemy idle
        GameObject spawn = Instantiate(enemy_to_spawn_gun, point1.position, point1.rotation);
        Instantiate(spawneffect, point1.position, point1.rotation);
        PlanetGravity pg = spawn.GetComponent<PlanetGravity>();
        pg.SetPlanet(GetComponent<PlanetGravity>().planet);
        EnemyShooting es = spawn.GetComponent<EnemyShooting>();
        es.SetPlayer(player);

        //Chase enemy
        GameObject spawn2 = Instantiate(enemy_to_spawn_patrolChase, point2.position, point2.rotation);
        Instantiate(spawneffect, point2.position, point2.rotation);
        PlanetGravity pg2 = spawn2.GetComponent<PlanetGravity>();
        pg2.SetPlanet(GetComponent<PlanetGravity>().planet);
        PatrolChase pc2 = spawn2.GetComponent<PatrolChase>();
        pc2.SetPlayer(player);
        pc2.SetChaseRange(GetComponent<PatrolChase>().MaxDist);
    }  

}

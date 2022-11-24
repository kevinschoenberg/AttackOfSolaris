using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public GameObject player;
    private readonly int EnemyMeleeDmg = 1;

    public static int MaxDist = 7;
    public static int MinDist = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {

        //If the player is close, attack
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        player.TryGetComponent<Health>(out Health HealthComponent);
        HealthComponent.TakeDamage(EnemyMeleeDmg);
    }



}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public GameObject player;
    [SerializeField] public float speed = 4.0f;


    private Vector3 _distanceToPLayer;

    private bool _isPlayerOnRightSide = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Find the player
        // use player location to get a direction (right or left)
        _distanceToPLayer = player.transform.position - transform.position;
        
        
        
        //work from here!!!!
        
        
        
        // Use that to change "_isPlayerOnRightSide" and walk in that direction

        //Walk towards the player


        //If the player is close, attack
    }



}

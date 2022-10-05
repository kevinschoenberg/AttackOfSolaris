using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public GameObject player;
    [SerializeField] public float speed = 4.0f;


    private Vector3 _distanceToPLayer;
    public int MoveSpeed = 4;
    public int MaxDist = 7;
    public int MinDist = 2;
    private int _isPlayerOnRightSide = 0;
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
        if (_distanceToPLayer[0] >= 0)
        {
            _isPlayerOnRightSide = -1;
        }
        else
        {
            _isPlayerOnRightSide = 1;
        }
        // Use that to change "_isPlayerOnRightSide" and walk in that direction
        //Walk towards the player
        if (Vector3.Distance(transform.position, player.transform.position) <= MaxDist)
        {
            transform.Translate(Vector2.left * (Time.deltaTime * MoveSpeed * _isPlayerOnRightSide), Space.Self);
        }
        else
        {
            transform.Translate(Vector2.zero, Space.Self);
        }

        //If the player is close, attack
        if (Vector3.Distance(transform.position, player.transform.position) <= MinDist) 
        {
            //Attack Player
        }
    }



}

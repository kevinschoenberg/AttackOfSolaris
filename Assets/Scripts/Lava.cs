using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public int damage = 1;
    public float hitDelay = .2f;
    private float _lastHit = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && _lastHit + hitDelay < Time.timeSinceLevelLoad)
        {
            playerHealth.TakeDamage(damage);
            _lastHit = Time.timeSinceLevelLoad;
        }
    }
}
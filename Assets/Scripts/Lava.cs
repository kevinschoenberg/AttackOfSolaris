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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && _lastHit + hitDelay < Time.timeSinceLevelLoad)
        {
            playerHealth.TakeDamage(damage);
            _lastHit = Time.timeSinceLevelLoad;
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    public float bulletTime = 10f;
    public int bulletDamageEnemy = 1;
    public int bulletDamagePlayer = 1;
    private float _createTime;
    private void Start()
    {
        _createTime = Time.time;
    }
    
    

    private void Update()
    {
        if (Time.time > _createTime + bulletTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Health>(out Health HealthComponent))
        {
            HealthComponent.TakeRangedDamage(bulletDamageEnemy);
        }
        else if (collision.gameObject.TryGetComponent(out PlayerHealth PlayerHealthComponent))
        {
            PlayerHealthComponent.TakeDamage(bulletDamagePlayer);
        }

        Destroy(gameObject);
    }
}

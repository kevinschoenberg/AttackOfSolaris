using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    private readonly int _bulletDamage = 1;
    private float _createTime;
    private void Start()
    {
        _createTime = Time.time;
    }
    
    

    private void Update()
    {
        if (Time.time > _createTime + 5f)
        {
            Destroy(gameObject);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
        {
            
            enemyComponent.TakeDamage(_bulletDamage);
        }
        
        Destroy(gameObject);
    }
}

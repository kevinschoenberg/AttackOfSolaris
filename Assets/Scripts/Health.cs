using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public int health;
    [SerializeField] public int maxHealth;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
           // player or enimy dies
        }
    }
}

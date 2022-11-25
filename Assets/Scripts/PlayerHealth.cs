using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int maxHealth = 10;
    public HealthBar healthbar;
    public GameObject DeathPanel;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthbar.SetMaxHealth(maxHealth);        
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        healthbar.SetHealth(health);
        if(health <= 0)
        {
            //Destroy(gameObject);
            Pause_Menu.PlayerDiedPause();
            DeathPanel.SetActive(true);
        }

    }
}

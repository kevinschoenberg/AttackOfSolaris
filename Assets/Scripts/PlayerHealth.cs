using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;


public class PlayerHealth : MonoBehaviour, ISaveable
{
    public int health;
    public int maxHealth = 10;
    public HealthBar healthbar;
    public GameObject DeathPanel;
    DeathCounter deathCounter;

    public float healdelay = 5f;
    public int healammount = 1;
    
    private float regen_timer;

    public bool rover_found = false;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
        deathCounter = GameObject.Find("StageManager").GetComponent<DeathCounter>();
        if (PlayerPrefs.GetInt("HasMarsRover") == 1)
        {
            rover_found = true;
            transform.Find("Canvas/Rover").gameObject.SetActive(true);
        } 
    }
   
    void Update()
    {
        healthbar.SetHealth(health);

        if (rover_found && regen_timer + healdelay < Time.time)
        {
            regen_timer = Time.time;
            health += healammount;
            //hp regen
            
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if(health <= 0)
        {
            //Destroy(gameObject);
            deathCounter.PlayerDeath(1);
            Pause_Menu.PlayerDiedPause();
            DeathPanel.SetActive(true);
        }
    }

    public object SaveState()
    {
        return new SaveData()
        {
            health = this.health,
            maxHealth = this.maxHealth,
        };

    }

    public void LoadState(object state)
    {
        var saveData = (SaveData)state;
        health = saveData.health;
        maxHealth = saveData.maxHealth;
    }

    [Serializable]
    private struct SaveData
    {
        public int health;
        public int maxHealth;
    }
}
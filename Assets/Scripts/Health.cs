using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Unity.Mathematics;

public class Health : MonoBehaviour, ISaveable
{
    [SerializeField] public float health;
    [SerializeField] public int maxHealth;
    [SerializeField] private float rangedDamageAmp = 1;
    GameObject enemy;
    public HealthBar healthbar;
    killcounter killcounterscript;
    private int scoreValue = 0;
    public bool Dead;

    // Start is called before the first frame update
    void Start()
    {
        Dead = false;
        //n�r man starter spillet bliver health initialiseret 
        health = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
        killcounterscript = GameObject.Find("StageManager").GetComponent<killcounter>();
        enemy = gameObject;
    }

    public void Update()
    {
        healthbar.SetHealth(health);
        if(Dead)    {enemy.SetActive(false);}
        else        {enemy.SetActive(true);}
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if (health <= 0 && !Dead)
        {
            if(enemy.tag == "Command_Enemy")    {scoreValue = 100;}
            else if(enemy.tag == "Tech_hub")    {scoreValue = 100;}
            else                                {scoreValue = 1;}
            Dead = true;
            killcounterscript.Enemykilled(scoreValue);
        }
    }

    public void TakeRangedDamage(int damageAmount)
    {
        TakeDamage(damageAmount * rangedDamageAmp);
    }

    public object SaveState()
    {
        return new SaveData()
        {
            health = this.health,
            maxHealth = this.maxHealth,
            Dead = this.Dead
        };

    }

    public void LoadState(object state)
    {
        var saveData = (SaveData)state;
        health = saveData.health;
        maxHealth = saveData.maxHealth;
        Dead = saveData.Dead;
    }

    [Serializable]
    private struct SaveData
    {
        public float health;
        public int maxHealth;
        public bool Dead;
    }
}

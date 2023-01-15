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
    CapsuleCollider2D[] collider2d;
    BoxCollider2D boxcollider2d;
    PlanetGravity planetGravity;
    SpriteRenderer sprite;
    GameObject canvas;
    GameObject gunRotation;
    EnemyShooting enemyShooting;
    bool gunEnemy;
    bool pg;
    PatrolChase patrolChase;
    bool isPatrolChase;
    bool isPatrolIgnore;
    PatrolIgnore patrolIgnore;


    // Start is called before the first frame update
    void Start()
    {
        gunEnemy = TryGetComponent<EnemyShooting>(out enemyShooting);
        pg = TryGetComponent<PlanetGravity>(out planetGravity);
        sprite = GetComponent<SpriteRenderer>();
        if (gunEnemy)
        {
            boxcollider2d = GetComponent<BoxCollider2D>();
            gunRotation = transform.Find("Gun_Rotation_Point").gameObject;
        }
        else
        {
            collider2d = GetComponents<CapsuleCollider2D>();
        }
        TryGetComponent<PatrolChase>(out patrolChase);
        TryGetComponent<PatrolIgnore>(out patrolIgnore);
        canvas = transform.Find("Canvas").gameObject;
        Dead = false;
        //n�r man starter spillet bliver health initialiseret 
        health = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
        killcounterscript = GameObject.Find("StageManager").GetComponent<killcounter>();
        enemy = gameObject;
    }

    void Update()
    {
        sprite = GetComponent<SpriteRenderer>();
        healthbar.SetHealth(health);
        if(Dead)    
        {
            if (gunEnemy)
            {
                enemyShooting.enabled = false;
                gunRotation.SetActive(false);
                boxcollider2d.enabled = false;
            }
            else
            {
                foreach (CapsuleCollider2D coll in collider2d)
                {
                    coll.enabled = false;
                }
            }
            if (isPatrolChase)
            {
                patrolChase.enabled = false;
            }
            else if (patrolIgnore)
            {
                patrolIgnore.enabled = false;
            }
            if (pg)
            {
                planetGravity.enabled = false;
            }

            sprite.enabled = false;
            canvas.SetActive(false);
        }
        else     
        {
            if (gunEnemy)
            {
                enemyShooting.enabled = true;
                gunRotation.SetActive(true);
                boxcollider2d.enabled = true;
            }
            else
            {
                foreach (CapsuleCollider2D coll in collider2d)
                {
                    coll.enabled = true;
                }
            }
            if (isPatrolChase)
            {
                patrolChase.enabled = true;
            }
            else if (patrolIgnore)
            {
                patrolIgnore.enabled = true;
            }
            if (pg)
            {
                planetGravity.enabled = true;
            }
            sprite.enabled = true;
            canvas.SetActive(true);
            GameObject.Find("StageManager/SaveLoadSystem").GetComponent<SaveLoadSystem>().justLoad = false;
        }
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

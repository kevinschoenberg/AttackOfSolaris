using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Health : MonoBehaviour, ISaveable
{
    [SerializeField] public int health;
    [SerializeField] public int maxHealth;
    public GameObject Enemy;
    public HealthBar healthbar;
    killcounter killcounterscript;
    private int score_value = 1;

    //Following are used for alternate death so that the objects can be saved after death
    public bool Dead;
    bool patrol;
    SpriteRenderer SR;
    BoxCollider2D BC2;
    Animator A;
    PatrolChase PC;
    PatrolIgnore PI;
    EnemyDmg ED;



    // Start is called before the first frame update
    void Start()
    {
        Dead = false;
        SR = GetComponent<SpriteRenderer>();
        BC2 = GetComponent<BoxCollider2D>();
        PC = GetComponent<PatrolChase>();
        A = GetComponent<Animator>();
        ED = GetComponent<EnemyDmg>();

        //n�r man starter spillet bliver health initialiseret 
        health = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
        killcounterscript = GameObject.Find("StageManager").GetComponent<killcounter>();
        Enemy = gameObject;
        if(Enemy.tag == "Command_Enemy")
        {
            score_value = 100;
        }
    }

    public void Update()
    {
        healthbar.SetHealth(health);

        if(Dead)
        {
            if(TryGetComponent<PatrolChase>(out var PC))
            {
                PC.enabled = false;
            }
            else if(TryGetComponent<PatrolIgnore>(out var PI))
            {
                PI.enabled = false;
            }
            SR.enabled = false;
            BC2.enabled = false;
            PI.enabled = false;
            A.enabled = false;

        }
        else
        {
            if(TryGetComponent<PatrolChase>(out var PC))
            {
                PC.enabled = true;
            }
            else if(TryGetComponent<PatrolIgnore>(out var PI))
            {
                PI.enabled = true;
            }
            SR.enabled = true;
            BC2.enabled = true;
            ED.enabled = true;
            A.enabled = true;

        }
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Dead = true;
            killcounterscript.Enemykilled(score_value);
        }
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
        public int health;
        public int maxHealth;
        public bool Dead;
    }
}

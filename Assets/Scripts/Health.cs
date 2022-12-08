using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] public int health;
    [SerializeField] public int maxHealth;
    public GameObject Enemy;
    public HealthBar healthbar;
    killcounter killcounterscript;
    private int score_value = 1;

    // Start is called before the first frame update
    void Start()
    {
        //når man starter spillet bliver health initialiseret 
        health = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
        killcounterscript = GameObject.Find("StageManager").GetComponent<killcounter>();
        Enemy = gameObject;
        if(Enemy.tag == "Command_Enemy")
        {
            score_value = 100;
        }
    }
    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        healthbar.SetHealth(health);

        if (health <= 0)
        {

            killcounterscript.Enemykilled(score_value);
            Destroy(gameObject);

        }
    }
}

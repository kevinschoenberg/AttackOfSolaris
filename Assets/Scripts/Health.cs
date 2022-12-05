using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] public int health;
    [SerializeField] public int maxHealth;
    public HealthBar healthbar;
    killcounter killcounterscript;

    // Start is called before the first frame update
    void Start()
    {
        //når man starter spillet bliver health initialiseret 
        health = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
        killcounterscript = GameObject.Find("StageManager").GetComponent<killcounter>();

    }
    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        healthbar.SetHealth(health);

        if (health <= 0)
        {

            killcounterscript.Enemykilled();
            Destroy(gameObject);

        }
    }
}

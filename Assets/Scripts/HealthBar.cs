using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    public Slider HealthSlider; 
    // Start is called before the first frame update
    
    public void SetMaxHealth(int maxhealth)
    {
        HealthSlider.maxValue = maxhealth;
        HealthSlider.value = maxhealth;
    }
    public void SetHealth(float health)
    {
        HealthSlider.value = health;
    }
}

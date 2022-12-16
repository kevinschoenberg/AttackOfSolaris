using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FuelBar : MonoBehaviour
{
    public Slider FuelSlider; 
    // Start is called before the first frame update
    
    public void SetMaxFuel(float maxFuel)
    {
        FuelSlider.maxValue = maxFuel;
        FuelSlider.value = maxFuel;
    }
    public void SetFuel(float fuel)
    {
        FuelSlider.value = fuel;
    }
}

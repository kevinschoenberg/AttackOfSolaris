using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Begin_new_game : MonoBehaviour
{
    int restet = 0;
    //reseter alle playprefs som har noget med det nye spil at gøre
    public void Reset_prefs()
    {
        
        PlayerPrefs.SetInt("deathCount", restet);
        PlayerPrefs.SetInt("HasMarsRover", restet);
    }
}

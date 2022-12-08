using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killcounter : MonoBehaviour
{
    // Start is called before the first frame update
    public int score = 0;
    public GameObject LevelCompletePanel;
    public int scorre_threshold = 100;


    public void Enemykilled(int points)
    {
        
        score += points;
        if (score >= scorre_threshold)
        {
            Pause_Menu.PlayerDiedPause();
            LevelCompletePanel.SetActive(true);
        }
    }

    // Start is called before the first frame update

}

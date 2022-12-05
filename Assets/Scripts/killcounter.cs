using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killcounter : MonoBehaviour
{
    // Start is called before the first frame update
    public int kills = 0;
    public GameObject LevelCompletePanel;
    public int scorre_threshold = 2;

    public void Enemykilled()
    {
        kills++;
        if (kills > scorre_threshold)
        {
            Pause_Menu.PlayerDiedPause();
            LevelCompletePanel.SetActive(true);
        }
    }

    // Start is called before the first frame update

}

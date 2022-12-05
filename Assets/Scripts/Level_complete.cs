using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_complete : MonoBehaviour
{
    // Start is called before the first frame update
    killcounter killcounterscript;
    public GameObject LevelCompletePanel;
    int kills;
    public int threshold;
    void Start()
    {
        killcounterscript = GameObject.Find("StageManager").GetComponent<killcounter>();

        kills = killcounterscript.kills;

        if (kills >= threshold)
            Pause_Menu.PlayerDiedPause();
            LevelCompletePanel.SetActive(true);

    }

}

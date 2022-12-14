using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class killcounter : MonoBehaviour, ISaveable
{
    // Start is called before the first frame update
    public int score = 0;
    public Text scoreText;
    public GameObject LevelCompletePanel;
    public int scorre_threshold = 100;

    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        int enemiesLeft = enemies.Length;
        scoreText.text = "Score: " + score.ToString() + "/" + scorre_threshold + ": Out of " + enemiesLeft + " Enemies";
    }

    public void Enemykilled(int points)
    {
        
        score += points;
        if (score >= scorre_threshold)
        {
            Pause_Menu.PlayerDiedPause();
            LevelCompletePanel.SetActive(true);
        }
    }

    public object SaveState()
    {
        return new SaveData()
        {
            score = this.score
        };

    }

    public void LoadState(object state)
    {
        var saveData = (SaveData)state;
        score = saveData.score;
    }

    [Serializable]
    private struct SaveData
    {
        public int score;
    }
}

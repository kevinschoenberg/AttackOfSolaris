using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DeathCounter : MonoBehaviour, ISaveable
{
    public Text deathCounterText;
    public int deathCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        deathCount = PlayerPrefs.GetInt("deathCount");
    }

    // Update is called once per frame
    void Update()
    {
        deathCounterText.text = "Player Death Count: " + deathCount;
    }

    public void PlayerDeath(int points)
    {
        
        deathCount += points;
        PlayerPrefs.SetInt("deathCount", deathCount);
    }

    public object SaveState()
    {
        return new SaveData()
        {
            deathCount = this.deathCount
        };

    }

    public void LoadState(object state)
    {
        var saveData = (SaveData)state;
        deathCount = saveData.deathCount;
    }

    [Serializable]
    private struct SaveData
    {
        public int deathCount;
    }

}



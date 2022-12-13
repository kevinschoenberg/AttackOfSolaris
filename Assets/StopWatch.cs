using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class StopWatch : MonoBehaviour
{
    bool StopWatchActive = true;
    float currentTime;
    public Text currentTimeText;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (StopWatchActive) 
        {
            currentTime = currentTime + Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = time.Minutes.ToString() + ":" + time.Seconds.ToString();
        
    }

    public void startStopWatch()
    {
        StopWatchActive = true;
    }

    public void stopStopWatch()
    {
        StopWatchActive = false;
    }
}

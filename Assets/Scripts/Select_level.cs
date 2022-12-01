using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;


public class Select_level : MonoBehaviour
{
    public int level;
    [SerializeField] public Smooth_Transition Smooth_Trans;
    private long timer;
    
    public void select_level()
    {
        timer = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        Smooth_Trans.SwapSound();
        while (level == 1 && (DateTimeOffset.Now.ToUnixTimeMilliseconds() - timer) < 2000)
        {
            SceneManager.LoadScene(level);
        }
        SceneManager.LoadScene(level);
    }
}

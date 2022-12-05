using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pause_Menu : MonoBehaviour
{
    public static bool IsPaused = false;
    public GameObject PausePanel;
    static SaveLoadSystem SLS;

    void start()
    {
        SLS = GameObject.FindGameObjectWithTag("SaveLoadSystem").GetComponent<SaveLoadSystem>();
    }
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.P))
        {
            if (IsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        PausePanel.SetActive(false);
        IsPaused = false;
        Time.timeScale = 1f;
    }
    public void Pause()
    {
        PausePanel.SetActive(true);
        IsPaused = true;
        Time.timeScale = 0;
    }
    public static void PlayerDiedPause()
    {
        IsPaused = true;
        Time.timeScale = 0;
    }
    public static void Save()
    {
        SLS.Save();
    }
    public static void Load()
    {
        SLS.Load();
    }
}

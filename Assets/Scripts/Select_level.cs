using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Select_level : MonoBehaviour
{
    public int level;
    
    public void select_level()
    {
        SceneManager.LoadScene(level);
    }
}


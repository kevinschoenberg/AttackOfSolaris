using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Select_level : MonoBehaviour
{
    public int level;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void select_level()
    {
        SceneManager.LoadScene(level);
    }
    
}

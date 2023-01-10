using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NextScene : MonoBehaviour
{
    public void Start()
    {
        int current_scene = SceneManager.GetActiveScene().buildIndex;
        if (current_scene != 10)
            SceneManager.LoadScene(current_scene + 1);
        else
            SceneManager.LoadScene(0);
    }
}
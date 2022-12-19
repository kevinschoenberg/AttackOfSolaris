using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NextScene : MonoBehaviour
{
    public int level;
    public void Start()
    {
        int current_scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(current_scene + 1);
    }
}
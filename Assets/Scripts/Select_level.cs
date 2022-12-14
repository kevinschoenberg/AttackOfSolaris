using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Select_level : MonoBehaviour
{
    public int level;
    int new_game = 1;
    public TMP_InputField inputField;
    string playername;

    public void select_level()
    {
        SceneManager.LoadScene(level);
    }
    public void next_level()
    {
        SceneManager.LoadScene(level + 1);
    }
    public void Begin_new_game()
    {
        SceneManager.LoadScene(new_game);
    }
    public void Save_Name()
    {
        string playername = inputField.text;
        Debug.Log(playername);
        PlayerPrefs.SetString("playername", playername);
 
    }

}


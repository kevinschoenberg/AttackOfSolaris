using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death_back : MonoBehaviour
{
    public int Menu;
    public void GoBack()
    {
        SceneManager.LoadScene(Menu);
    }
}

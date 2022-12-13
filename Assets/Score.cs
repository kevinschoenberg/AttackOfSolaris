using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public killcounter score;
    public Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        int enemiesLeft = enemies.Length;
        scoreText.text = "Score: " + score.score.ToString() + "/" + score.scorre_threshold + ": Out of " + enemiesLeft + " Enemies";
    }
}

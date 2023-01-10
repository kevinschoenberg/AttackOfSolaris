using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MarsRover : MonoBehaviour
{
    public GameObject player;
    
    public GameObject Rover;

    string scene;

    [SerializeField] PlayerHealth playerhealthscript;

    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene().name;
        if (scene == "Mars")
        {
            PlayerPrefs.SetInt("HasMarsRover", 0);
        }
        if (PlayerPrefs.GetInt("HasMarsRover") == 1)
        {
            playerhealthscript.rover_found = true;
            player.transform.Find("Canvas/Rover").gameObject.SetActive(true);
        } 

        
    }

    // Update is called once per frame
    void Update()
    {
        float Dist_player_rover = Vector2.Distance(Rover.transform.position, player.transform.position);
        if (Dist_player_rover < 3f)
        {
            playerhealthscript.rover_found = true;
            PlayerPrefs.SetInt("HasMarsRover", 1);
            pickuprover();
        }
    }
    private void pickuprover()
    {
        Transform pet_rover = player.transform.Find("Canvas/Rover");
        pet_rover.gameObject.SetActive(true);
        Rover.SetActive(false);
    }
}

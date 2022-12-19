using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarsRover : MonoBehaviour
{
    public GameObject player;
    
    public GameObject Rover;
    

    private bool has_rover = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float Dist_player_rover = Vector2.Distance(Rover.transform.position, player.transform.position);
        if (Dist_player_rover < 3f)
        {
            has_rover = true;
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

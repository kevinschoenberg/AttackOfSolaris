using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Objective_mars : MonoBehaviour
{
    public killcounter kilcounterscript;
    public GameObject TechHub;
    public Transform rocket_marker;
    public GameObject player;

    private bool TecHub_destroyed = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float Dist_player_rocket = Vector2.Distance(rocket_marker.position, player.transform.position);

        if(TechHub.IsDestroyed())
        {
            TecHub_destroyed = true;
        }

        if ((Dist_player_rocket < 3f && TecHub_destroyed))
        {
            //win game
            kilcounterscript.score = kilcounterscript.score_threshold;
        }
    }
}

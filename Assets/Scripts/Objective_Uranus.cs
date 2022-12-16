using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Objective_Uranus : MonoBehaviour
    //script is applied to the jetpack on ground
{
    // Start is called before the first frame update
    public GameObject player;
    private PlayerMovement PlayerMove;
    public Transform jetpack;
    public Transform rocket_marker;
    public killcounter kilcounterscript;

    private bool has_fueltank = false;
    private bool returned_to_rocket = false;
    void Start()
    {
        PlayerMove = player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        float Dist_player_jetpack = Vector2.Distance(jetpack.position, player.transform.position);

        if (Dist_player_jetpack < 3f)
        {
            has_fueltank = true;
        }
        float Dist_player_rocket_marker = Vector2.Distance(rocket_marker.position, player.transform.position);
        if (Dist_player_rocket_marker < 3f && has_fueltank)
        {
            returned_to_rocket = true;
        }
        if (has_fueltank && returned_to_rocket)
        {
            //win level
            kilcounterscript.score = kilcounterscript.score_threshold;
        }
    }
}

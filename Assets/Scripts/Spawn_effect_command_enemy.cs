using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_effect_command_enemy : MonoBehaviour
{
    public ParticleSystem spawneffect;
    public AudioClip spawnsound;

    public void activate_spawn_effect()
    {
        Instantiate(spawneffect, transform.position, transform.rotation);
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class IsScenePlaying : MonoBehaviour
{
    private PlayableDirector Director;
    private bool playing = false;
    // Use this for initialization
    void Start()
    {
        Director = GetComponent<PlayableDirector>();
    }

    void Update()
    {
        if (playing == false)
        {
            Director.Play();
            playing = true;
        }
    }
}

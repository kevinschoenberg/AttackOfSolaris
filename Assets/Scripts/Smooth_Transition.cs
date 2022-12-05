using System.Collections;
using UnityEngine;

public class Smooth_Transition : MonoBehaviour
{
    public AudioSource Sound;

    [SerializeField] private float normalVolume;
    [SerializeField] private float transitionTime;

    private void Start()
    {
        if (Sound.name == "Music")
        {
            SwapSound();
        }
    }
    public void SwapSound()
    {
        AudioSource current = Sound;
        StartCoroutine(TransitionSound(current));
    }

    IEnumerator TransitionSound(AudioSource current)
    {
        float percentage = 0;
        if (current.volume > 0)
        {
            while (current.volume > 0)
            {
                current.volume = Mathf.Lerp(normalVolume, 0, percentage);
                percentage += Time.deltaTime / transitionTime;
                yield return null;
            }
            current.Pause();
        }
        else if (current.volume == 0)
        {
            while (current.volume < normalVolume)
            {
                current.volume = Mathf.Lerp(0, normalVolume, percentage);
                percentage += Time.deltaTime / transitionTime;
                yield return null;
            }
        }
    }
    
}
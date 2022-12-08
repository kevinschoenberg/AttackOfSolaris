using System.Collections;
using UnityEngine;

public class Smooth_Transition : MonoBehaviour
{
    public AudioSource Sound;
    public float startingVolume = 0.5f;
    public float normalVolume = 0.5f;
    private float transitionTime = 5f;

    public void AdjustVolume(float Volume)
    {
        startingVolume = normalVolume;
        normalVolume = Volume;
        SwapSound();
    }

    private void Start()
    {
        if (Sound.name == "MarsMusic" || Sound.name == "MenuMusic"  || Sound.name == "MoonMusic")
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
        if (current.volume > normalVolume || current.volume < normalVolume)
        {
            while (current.volume != normalVolume)
            {
                current.volume = Mathf.Lerp(startingVolume, normalVolume, percentage);
                percentage += Time.deltaTime / transitionTime;
                yield return null;
            }
            current.Pause();
        }
        else if (current.volume > 0)
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
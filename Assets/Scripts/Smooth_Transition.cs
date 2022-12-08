using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Smooth_Transition : MonoBehaviour
{
    public AudioSource Sound;
    private float startingVolume;
    public float normalVolume;
    private float transitionTime = 5f;
    AdjustVolume VolumeSystem;
    public Slider sliderInstance;
    public void AdjustVolume(float Volume)
    {
        VolumeSystem.SetVolume(Volume);
        SwapSound();
    }

    private void Start()
    {
        VolumeSystem = FindObjectOfType<AdjustVolume>();
        if (Sound.name == "MarsMusic" || Sound.name == "MenuMusic"  || Sound.name == "MoonMusic")
        {
            SwapSound();
        }
    }
    public void SwapSound()
    {
        VolumeSystem = FindObjectOfType<AdjustVolume>();
        AudioSource current = Sound;
        StartCoroutine(TransitionSound(current));
    }

    IEnumerator TransitionSound(AudioSource current)
    {        
        float percentage = 0;
        if (VolumeSystem.GetVolume() == 0)
            VolumeSystem.SetVolume(normalVolume);
        normalVolume = VolumeSystem.GetVolume();
        if (sliderInstance != null)
            sliderInstance.value = VolumeSystem.GetVolume();
        if (current.volume == 0)
        {
            while (current.volume < normalVolume)
            {
                current.volume = Mathf.Lerp(0, normalVolume, percentage);
                percentage += Time.deltaTime / transitionTime;
                yield return null;
            }
        }
        else if (current.volume > normalVolume || current.volume < normalVolume)
        {
            startingVolume = current.volume;
            while (current.volume != normalVolume)
            {
                current.volume = normalVolume;
                yield return null;
            }
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
    }
    
}
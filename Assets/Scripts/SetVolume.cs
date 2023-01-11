using System.Collections;
using UnityEngine;

public class SetVolume : MonoBehaviour
{
    public AudioSource[] Audios;
    AdjustVolume VolumeSystem;
    void Start()
    {
        VolumeSystem = FindObjectOfType<AdjustVolume>();
        for (int i =0; i<Audios.Length; i++)
        {
            Audios[i].volume = PlayerPrefs.GetFloat("Volume");
        } 
    }
}
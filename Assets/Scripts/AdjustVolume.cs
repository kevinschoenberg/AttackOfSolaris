using System.Collections;
using UnityEngine;

public class AdjustVolume : MonoBehaviour
{
    string volumeKey = "Volume";

    public float GetVolume()
    {
        return PlayerPrefs.GetFloat(volumeKey);
    }

    public void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat(volumeKey, volume);
    }
}
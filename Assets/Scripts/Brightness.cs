using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class Brightness : MonoBehaviour
{
    public Slider brightnessslider;
    public PostProcessProfile brightness;
    public PostProcessLayer layer;
    public float global_exposure; //the global brightness

    AutoExposure exposure;

    // Start is called before the first frame update
    void Start()
    {
        brightness.TryGetSettings(out exposure);
        AddjustBrightsness(brightnessslider.value);
    }

    public void AddjustBrightsness(float value)
    {
        if (value != 0)
        {
            exposure.keyValue.value = value;
            global_exposure = value;
            PlayerPrefs.SetFloat("brightness", global_exposure);
        }
        else
        {
            exposure.keyValue.value = 0.5f; /*So you cant make it pitch blackk */
            global_exposure = 0.5f;
            PlayerPrefs.SetFloat("brightness", global_exposure);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Brightness_default : MonoBehaviour
{
    public PostProcessProfile brightness;
    public PostProcessLayer layer;

    AutoExposure exposure; //fix ekstra camera delen
    void Start()
    {
        exposure.keyValue.value = PlayerPrefs.GetFloat("brightness");
    }

}

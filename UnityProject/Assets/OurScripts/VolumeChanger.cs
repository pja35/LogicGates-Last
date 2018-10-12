using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeChanger : MonoBehaviour
{
    private AudioSource manager;
    private ParametersLoader param;

    public void Start()
    {
        manager = (AudioSource)GameObject.FindGameObjectsWithTag("Speaker")[0].GetComponent(typeof(AudioSource));
        param = (ParametersLoader)GameObject.FindGameObjectsWithTag("Parameters")[0].GetComponent(typeof(ParametersLoader));
        GetComponent<Slider>().value = manager.volume;
    }

    public void OnChange()
    {
        if (manager != null)
        {
            manager.volume = GetComponent<Slider>().value;
            param.parameters.music_volume = GetComponent<Slider>().value;
        }
        else
        {
            Debug.Log("Manager not found");
        }
    }
}

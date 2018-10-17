using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeManager : MonoBehaviour
{

    void Start()
    {
        Debug.Log(ParametersLoader.GetMusicVolume());
        GetComponent<AudioSource>().volume = ParametersLoader.GetMusicVolume();
    }
}

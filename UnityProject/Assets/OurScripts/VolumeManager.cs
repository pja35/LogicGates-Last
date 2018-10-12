using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeManager : MonoBehaviour
{
    public ParametersLoader manager;

    void Start()
    {
        GetComponent<AudioSource>().volume = manager.parameters.music_volume;
    }
}

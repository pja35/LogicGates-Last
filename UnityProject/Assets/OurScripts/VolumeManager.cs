using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! Gestion des paramètres audio.
public class VolumeManager : MonoBehaviour
{
	//! Permet de modifier les paramètres.
    public void Start()
    {
        Debug.Log(ParametersLoader.GetMusicVolume());
        GetComponent<AudioSource>().volume = ParametersLoader.GetMusicVolume();
    }
}

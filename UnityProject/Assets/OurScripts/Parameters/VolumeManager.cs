using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Permet de gerer le volume du speaker.
/// </summary>
public class VolumeManager : MonoBehaviour
{
    /// <summary>
    /// Initialise la valeur du speaker avec celle sauvegardée dans les paramètres.
    /// </summary>
    public void Start()
    {
        Debug.Log(ParametersLoader.GetMusicVolume());
        GetComponent<AudioSource>().volume = ParametersLoader.GetMusicVolume();
    }
}

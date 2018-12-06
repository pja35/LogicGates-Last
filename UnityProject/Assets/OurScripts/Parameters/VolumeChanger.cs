using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Permet de modifier le volume du speaker.
/// </summary>
public class VolumeChanger : MonoBehaviour
{
    /// <summary>
    /// Le speaker qui sera modifié.
    /// </summary>
    private AudioSource speaker;

    /// <summary>
    /// Récupère le haut parler de unique du jeu et initialise la valeur du slider avec son volume.
    /// </summary>
    public void Start()
    {
        speaker = (AudioSource)GameObject.FindGameObjectsWithTag("Speaker")[0].GetComponent(typeof(AudioSource));
        GetComponent<Slider>().value = ParametersLoader.GetMusicVolume();
    }

    /// <summary>
    /// Si on modifie la valeur du slider on actualise le volume du speaker.
    /// </summary>
    public void OnChange()
    {
        if (speaker != null)
        {
            speaker.volume = GetComponent<Slider>().value;
            ParametersLoader.SetMusicVolume(GetComponent<Slider>().value);
        }
        else
        {
            Debug.Log("Spkeaker not found");
        }
    }
}

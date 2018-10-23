using UnityEngine;
using UnityEngine.UI;

//! Script permettant de modifier le volume.
public class VolumeChanger : MonoBehaviour
{
    private AudioSource speaker;

	//! Récupère le haut-parleur unique et de mettre la valeur du curseur assigné à cette tâche. 
    public void Start()
    {
        speaker = (AudioSource)GameObject.FindGameObjectsWithTag("Speaker")[0].GetComponent(typeof(AudioSource));
        GetComponent<Slider>().value = ParametersLoader.GetMusicVolume();
    }

	//! Modifie la puissance d'un haut-parleur.
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

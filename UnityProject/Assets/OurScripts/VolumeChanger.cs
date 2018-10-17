using UnityEngine;
using UnityEngine.UI;

public class VolumeChanger : MonoBehaviour
{
    private AudioSource speaker;

    public void Start()
    {
        speaker = (AudioSource)GameObject.FindGameObjectsWithTag("Speaker")[0].GetComponent(typeof(AudioSource));
        GetComponent<Slider>().value = ParametersLoader.GetMusicVolume();
    }

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

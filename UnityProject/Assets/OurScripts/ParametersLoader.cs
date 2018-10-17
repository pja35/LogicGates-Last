using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParametersLoader : MonoBehaviour
{
    public static Parameters parameters = new Parameters();

    /*Load parameters from containing file and set application max fps to 60*/
    public void Start()
    {
        Application.targetFrameRate = 60;

        parameters.LoadParameters();
        Debug.Log("loaded = " + parameters.music_volume + " " + parameters.snooze);

        GameObject.Find("Speaker").GetComponent<AudioSource>().volume = GetMusicVolume();
    }

    public static float GetMusicVolume() { return parameters.music_volume; }
    public static void SetMusicVolume(float music_volume) { parameters.music_volume = music_volume; }

    public static bool GetSnooze() { return parameters.snooze; }
    public static void SetSnooze(bool snooze) { parameters.snooze = snooze; }

}

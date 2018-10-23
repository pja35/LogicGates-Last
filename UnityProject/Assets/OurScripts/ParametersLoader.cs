using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Platform{PC,PHONE}

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
	
	public static Platform getPlatform(){
		string system = (SystemInfo.operatingSystem.Split(' '))[0];
		switch(system){
			case "Android":case "iPhone":
				return Platform.PHONE;
		}
		return Platform.PC;
	}
}

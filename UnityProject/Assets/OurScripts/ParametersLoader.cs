using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Platform{PC,PHONE}

//! Permet de charger et d'accéder aux paramètres du jeu depuis l'obet parameters.
public class ParametersLoader : MonoBehaviour
{
	//! Paramètre chargé au lancement du jeu.
    public static Parameters parameters = new Parameters();

    /** Charge es paramètres depuis un fichier et fixe le nombre d'ips max à 60*/
    public void Start()
    {
        Application.targetFrameRate = 60;

        parameters.LoadParameters();
        Debug.Log("loaded = " + parameters.music_volume + " " + parameters.snooze);

        GameObject.Find("Speaker").GetComponent<AudioSource>().volume = GetMusicVolume();
    }
	
	//! @return Volume du son
    public static float GetMusicVolume() { return parameters.music_volume; }
	//! Fixe le volume de la musique
    public static void SetMusicVolume(float music_volume) { parameters.music_volume = music_volume; }
	
	//! @return Etat du vibreur
    public static bool GetSnooze() { return parameters.snooze; }
	//! Fixe l'état du vibreur
    public static void SetSnooze(bool snooze) { parameters.snooze = snooze; }
	
	//! @return Type de plateforme sur laquelle l'application a été lancé.
	public static Platform getPlatform(){
		string system = (SystemInfo.operatingSystem.Split(' '))[0];
		switch(system){
			case "Android":case "iPhone":
				return Platform.PHONE;
		}
		return Platform.PC;
	}
}

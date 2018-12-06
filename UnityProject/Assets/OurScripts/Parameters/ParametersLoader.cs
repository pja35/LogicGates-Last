using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Platform { PC, PHONE }

/// <summary>
/// Un adapteur pour paramètre afin de pouvoir l'attacher à un objet en jeu.
/// </summary>
public class ParametersLoader : MonoBehaviour
{
    /// <summary>
    /// Paramètre chargé au lancement du jeu.
    /// </summary>
    public static Parameters parameters = new Parameters();

    /// <summary>
    /// Charge les paramètres et fixe les IPS max à 60.
    /// </summary>
    public void Start()
    {
        Application.targetFrameRate = 60;

        parameters.LoadParameters();

        GameObject.Find("Speaker").GetComponent<AudioSource>().volume = GetMusicVolume();
    }

    /// <summary>Renvoie le volume de la musique.</summary>
    /// <returns>Le volume de la musique.</returns>
    public static float GetMusicVolume() { return parameters.musicVolume; }

    /// <summary>
    /// Fixe le volume de la musique.
    /// </summary>
    /// <param name="music_volume">Le nouveau volume.</param>
    public static void SetMusicVolume(float music_volume) { parameters.musicVolume = music_volume; }

    /// <summary>Renvoie l'etat du vibreur.</summary>
    /// <returns>l'etat du vibreur.</returns>
    public static bool GetSnooze() { return parameters.snooze; }
    /// <summary>
    /// Fixe l'etat du vibreur.
    /// </summary>
    /// <param name="snooze">Le nouvel etat du vibreur.</param>
    public static void SetSnooze(bool snooze) { parameters.snooze = snooze; }

    /// <summary>
    /// Renvoi le nombre de niveaux du jeu.
    /// </summary>
    /// <returns>Le nombre de niveaux du jeu.</returns>
    public static int GetNbLevels() { return parameters.getNbLevels(); }

    /// <summary>
    /// Renvoi le nombre de niveaux débloqués.
    /// </summary>
    /// <returns>Le nombre de niveaux débloqués.</returns>
    public static int GetUnlockedLevels()
    {
        return parameters.getUnlockedLevels();
    }

    /// <summary>
    /// Débloque le niveau suivant.
    /// </summary>
    public static void UnlockLevel()
    {
        parameters.UnlockLevel();
        parameters.saveParameters();
    }

    /// <summary>
    /// Bloque le niveau actuel.
    /// </summary>
    public static void LockLevel()
    {
        parameters.lockLevel();
        parameters.saveParameters();
    }

    /// <summary>
    /// Sauvegarde les paramètres.
    /// </summary>
    public static void SaveParameters()
    {
        parameters.saveParameters();
    }


    /// <summary>
    /// Pour connaitre la plterforme sur laquelle on tourne.
    /// </summary>
    /// <returns>La plateforme.</returns>
    public static Platform getPlatform()
    {
        string system = (SystemInfo.operatingSystem.Split(' '))[0];
        switch (system)
        {
            case "Android":
            case "iPhone":
                return Platform.PHONE;
        }
        return Platform.PC;
    }

    /// <summary>
    /// Pour connaitre le niveau suivant par rapport au niveau actuel.
    /// </summary>
    /// <returns>Le nom du niveau suivant.</returns>
    public static string getNextLevelName(string currentSceneName)
    {
        double num = Char.GetNumericValue(currentSceneName, currentSceneName.Length - 1) + 1;
        string next = "Niveau" + num;
        return next;
    }
}

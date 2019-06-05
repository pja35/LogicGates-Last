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
    public static int GetNbLevels() { return parameters.GetNbLevels(); }

    /// <summary>
    /// Renvoi une couleur en fonction des paramètres enregistrés
    /// </summary>
    /// <returns></returns>
    public static Color GetColor() {
        if(parameters == null)
        {
            return new Color(0.009f, 0.724f, 1f);
        }
        return new Color(parameters.r, parameters.g, parameters.b);
    }

    /// <summary>
    /// Sauvegarde une couleur (utilisé pour les portes)
    /// </summary>
    public static void SetColor(Color color) {
        parameters.r = color.r;
        parameters.g = color.g;
        parameters.b = color.b;
    }

    /// <summary>
    /// Renvoi le nombre de niveaux débloqués.
    /// </summary>
    /// <returns>Le nombre de niveaux débloqués.</returns>
    public static int GetUnlockedLevels()
    {
        return parameters.GetUnlockedLevels();
    }

    /// <summary>
    /// Débloque le niveau suivant.
    /// </summary>
    public static void UnlockLevel()
    {
        parameters.UnlockLevel();
        parameters.SaveParameters();
    }

    /// <summary>
    /// Bloque le niveau actuel.
    /// </summary>
    public static void LockLevel()
    {
        parameters.LockLevel();
        parameters.SaveParameters();
    }

    /// <summary>
    /// Sauvegarde les paramètres.
    /// </summary>
    public static void SaveParameters()
    {
        parameters.SaveParameters();
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

    /// <summary>Renvoie l'etat du tutoriel.</summary>
    /// <returns>l'etat du tutoriel.</returns>
    public static bool GetTuto() { return parameters.tuto; }
    /// <summary>
    /// Inverse l'etat du tutoriel.
    /// </summary>
    public static void SetTuto() { parameters.tuto = !parameters.tuto; }

    /// <summary>Renvoie le nombre d'interrupteur et de lampe dans le BAS.</summary>
    /// <returns>le nombre d'interrupteur et de lampe dans le BAS.</returns>
    public static int GetBASSize() { return parameters.BASSize; }
    /// <summary>
    /// Assigne un nouveau nombre d'interrupteur et de lampe dans le BAS.
    /// </summary>
    public static void SetBASSize(int size) { parameters.BASSize = size; }

}

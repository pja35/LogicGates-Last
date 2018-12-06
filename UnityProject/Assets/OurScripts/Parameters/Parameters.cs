using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

/// <summary>
/// La classe de stockage pour les paramètres du jeu.
/// </summary>
[System.Serializable]
public class Parameters
{
    /// <summary>
    /// Volume de la musique.
    /// </summary>
    public float musicVolume;
    /// <summary>
    /// Etat du virbreur.
    /// </summary>
    public bool snooze;
    /// <summary>
    /// Couleur de l'interface.
    /// </summary>
    public float color;
    /// <summary>
    /// Nombres de niveaux existants.
    /// </summary>
    private int nbLevels = 50;
    /// <summary>
    /// Nombre de niveaux débloqués.
    /// </summary>
    private int UnlockedLevels = 1;

    /// <summary>
    /// Charge les paramètres depuis un fichier sérialisé.
    /// </summary>
    public void LoadParameters()
    {
        if (File.Exists(Application.persistentDataPath + "/parameters.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/parameters.gd", FileMode.Open);
            Parameters loaded = (Parameters)bf.Deserialize(file);
            Debug.Log("Paramets loaded, value = " + loaded.musicVolume + " " + loaded.snooze);
            this.musicVolume = loaded.musicVolume;
            this.snooze = loaded.snooze;
            this.color = loaded.color;
            this.UnlockedLevels = loaded.UnlockedLevels;
            file.Close();
        }
        else
        {
            Debug.Log("SETTINGS FILE DOES NOT EXIST");
        }
    }

    /// <summary>
    /// Sauvegarde les paramètres.
    /// </summary>
    public void saveParameters()
    {
        Debug.Log("Saving Parameters");
        ParametersLoader loader = (ParametersLoader)GameObject.FindGameObjectsWithTag("Parameters")[0].GetComponent(typeof(ParametersLoader));
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/parameters.gd");
        Debug.Log("test = " + ParametersLoader.parameters.musicVolume + " " + ParametersLoader.parameters.snooze);
        bf.Serialize(file, ParametersLoader.parameters);
        file.Close();
    }

    /// <summary>
    /// Modifie l'etat du vribreur.
    /// </summary>
    public void changeSnooze()
    {
        snooze = !snooze;
    }

    /// <summary>
    /// Récupere le nombre de niveaux existants.
    /// </summary>
    /// <returns>Le nombres de niveaux du jeu.</returns>
    public int getNbLevels()
    {
        return nbLevels;
    }

    /// <summary>
    /// Récupere le nombre de niveaux débloqués.
    /// </summary>
    /// <returns>Le nombres de niveaux débloqués.</returns>
    public int getUnlockedLevels()
    {
        return UnlockedLevels;
    }

    /// <summary>
    /// Débloque le niveau suivant.
    /// </summary>
    public void UnlockLevel()
    {
        UnlockedLevels++;
    }


    /// <summary>
    /// Bloque le niveau actuel.
    /// </summary>
    public void lockLevel()
    {
        UnlockedLevels--;
    }

}

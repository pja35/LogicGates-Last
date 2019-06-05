using UnityEngine;
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
    public float r = 0.009f;
    public float g = 0.724f;
    public float b = 1f;
    /// <summary>
    /// Nombres de niveaux existants.
    /// </summary>
    private readonly int nbLevels = 24;
    /// <summary>
    /// Nombre de niveaux débloqués.
    /// </summary>
    private int UnlockedLevels = 1;
    /// <summary>
    /// Affichage du popup tutoriel.
    /// </summary>
    public bool tuto = true;
    /// <summary>
    /// Nombre de lampes et d'interrupteur dans le mode BAS.
    /// </summary>
    public int BASSize = 2;

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
            this.r = loaded.r;
            this.g = loaded.g;
            this.b = loaded.b;
            this.tuto = loaded.tuto;
            this.UnlockedLevels = loaded.UnlockedLevels;
            this.BASSize = loaded.BASSize;
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
    public void SaveParameters()
    {
        Debug.Log("Saving Parameters");
        //ParametersLoader loader = (ParametersLoader)GameObject.FindGameObjectsWithTag("Parameters")[0].GetComponent(typeof(ParametersLoader));
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/parameters.gd");
        Debug.Log("test = " + ParametersLoader.parameters.musicVolume + " " + ParametersLoader.parameters.snooze + ParametersLoader.parameters.tuto + ParametersLoader.parameters.BASSize);
        bf.Serialize(file, ParametersLoader.parameters);
        file.Close();
    }

    /// <summary>
    /// Modifie l'etat du vribreur.
    /// </summary>
    public void ChangeSnooze()
    {
        snooze = !snooze;
    }

    /// <summary>
    /// Récupere le nombre de niveaux existants.
    /// </summary>
    /// <returns>Le nombres de niveaux du jeu.</returns>
    public int GetNbLevels()
    {
        return nbLevels;
    }

    /// <summary>
    /// Récupere le nombre de niveaux débloqués.
    /// </summary>
    /// <returns>Le nombres de niveaux débloqués.</returns>
    public int GetUnlockedLevels()
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
    public void LockLevel()
    {
        UnlockedLevels--;
    }

}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

/**
   \brief Permet de sauvegarder les paramètres.
	
 * Cette classe va permettre de sauvegarder les paramètres dans un fichier binaire 
 * à partir de l'objet parameter qui les stockent tous.
*/
public class SaveParam : MonoBehaviour
{
	//! Sauvegarde les paramètres.
    public void save_parameters()
    {
        Debug.Log("Saving Parameters");
        ParametersLoader loader = (ParametersLoader)GameObject.FindGameObjectsWithTag("Parameters")[0].GetComponent(typeof(ParametersLoader));
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/parameters.gd");
        Debug.Log("test = " + ParametersLoader.parameters.music_volume + " " + ParametersLoader.parameters.snooze);
        bf.Serialize(file, ParametersLoader.parameters);
        file.Close();
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
public class Parameters
{
    public float music_volume;
    public bool snooze;
    public float color;

    public void LoadParameters()
    {
        if (File.Exists(Application.persistentDataPath + "/parameters.gd"))
        {
            Debug.Log("FILE DOES EXIST");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/parameters.gd", FileMode.Open);
            Parameters loaded = (Parameters)bf.Deserialize(file);
            Debug.Log("Value = " + loaded.music_volume + " " + loaded.snooze);
            this.music_volume = loaded.music_volume;
            this.snooze = loaded.snooze;
            this.color = loaded.color;
            file.Close();
        }
        else
        {
            Debug.Log("FILE DOES NOT EXIST");
        }
    }

    public void change_snooze()
    {
        snooze = !snooze;
    }
}

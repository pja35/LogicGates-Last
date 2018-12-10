using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Pour éviter la destruction des gestionnaires de paramètres. </summary> 
public class DontDestroyParam : MonoBehaviour
{

    /// <summary>
    /// Pour savoir si l'objet existe déjà.
    /// </summary>
    private static bool created = false;

    /// <summary>Défini l'objet attaché comme un objet non-destructible. </summary> 
    public void Start()
    {
        if (!created)
        {
            created = true;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

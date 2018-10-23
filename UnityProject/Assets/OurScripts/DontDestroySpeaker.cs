using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! Classe pour éviter la destruction des haut-parleurs. 
public class DontDestroySpeaker : MonoBehaviour
{
    private static bool created = false;

    //! Fixe le haut-parleur comme un objet non-destructible.
    void Start()
    {
        if (!created)
        {
            Debug.Log("Speaker Should not be Destroyed");
            created = true;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! Protège un objet de la destruction
public class DontDestroyParam : MonoBehaviour
{
	
	// Booléen autorisant la destruction d'un objet
    private static bool created = false;

    //! Protège l'objet attaché
    public void Start()
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

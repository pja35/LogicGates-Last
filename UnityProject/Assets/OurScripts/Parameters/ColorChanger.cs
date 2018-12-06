using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Pour changer la couleur enregistrée par paramètre. Inutilisé pour le moment.
/// </summary>
public class ColorChanger : MonoBehaviour
{
    //! Paramètre contenant la gestion de la couleur
    public Parameters manager;
    //! Initialise la couleur comme celle qu'elle était lors de la dernière sauvegarde 
    public void Start()
    {
        GetComponent<Image>().color = new Color(manager.color, manager.color, manager.color);
    }
}

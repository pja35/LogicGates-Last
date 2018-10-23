using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//! Permet de fermer le jeu depuis un bouton.
public class Quitter : MonoBehaviour
{
	//! Ferme le jeu en fonction de son environnement.
    public void OnClick()
    {
        Debug.Log("On a cliqué sur le bouton");
        
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                    Application.Quit();
        #endif
        
    }
}
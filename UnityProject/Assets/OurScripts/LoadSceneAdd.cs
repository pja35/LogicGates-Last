using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

/** 
	\brief Classe permettant le chargement d'une scène supperposé sur celle ou l'on se trouve.
	
	Ce script existe en complément de la \sa LoadScene car il permet de supperposé deux scènes.
*/
public class LoadSceneAdd : MonoBehaviour {

	//! Scène qui sera chargée.
    public string sceneToLoad;
	
	//! Charge la scène \sa sceneToLoad lorsque l'on clique dessus l'objet, par dessus celle actuelle.
    public void OnClick()
    {
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);
    }

}

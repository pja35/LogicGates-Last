using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

//!Decharge une scène
public class RemoveScene : MonoBehaviour {
	//! Scène qui va être déchargée.
    public string sceneToRemove;
	//! Décharge la scène \sa sceneToRemove .
    public void OnClick()
    {
        SceneManager.UnloadSceneAsync(sceneToRemove);
    }
}

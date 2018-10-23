using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Quitter : MonoBehaviour
{

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
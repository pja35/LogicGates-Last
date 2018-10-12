using UnityEngine;
using System.Collections;

public class Quitter : MonoBehaviour
{

    public void onClick()
    {
        // Save game data

        // Close game
        Debug.Log("On as cliqué sur le bouton");
        Application.Quit();
    }
}
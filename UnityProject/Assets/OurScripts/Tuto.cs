using UnityEngine;
/// <summary>
/// Classe permettant d'afficher une fenetre de tutoriel dans la scene. 
/// </summary>
public class Tuto : MonoBehaviour
{

    /// <summary>
    /// La fenetre pop-up qui va servir de tutoriel.
    /// </summary>
    public GameObject popup;

    /// <summary>
    /// Des la premiere frame on affiche la popup seulement si la variable tuto est activée dans les
    /// parametres utilisateur.
    /// </summary>
    void Start()
    {
        SetStateOfAllColliders(false);
        if (ParametersLoader.GetTuto())
        {
            popup.SetActive(true);   
        }
        else
        {
            Destroy(popup);
        }
    }

    //Utilise pour empécher de cliquer sur des objets pendant que le pop-up est présent
    private void SetStateOfAllColliders(bool state)
    {
        GameObject canvas = GameObject.Find("Canvas");
        Collider[] colliders = canvas.GetComponentsInChildren<Collider>();
        foreach (Collider colliderAct in colliders)
        {
            colliderAct.enabled = state;
        }
    }

    private void OnDestroy()
    {
        SetStateOfAllColliders(true);
    }



}

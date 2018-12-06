using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

/// <summary>
/// Permet de changer l'apparence de la lampe en fonction du courant et d'afficher la fenetre de victoire si elle est allumee.
/// </summary>
public class Lamp : MonoBehaviour, DevObjInit, Notifiable
{
    Image image;
    Obj_Input oi;
    public bool power = false;
    public Sprite textureOn;
    public Sprite textureOff;
    public GameObject popup;

    // Use this for initialization
    public void Instantiate()
    {
        image = GetComponent<Image>();
        oi = Obj_Input.createInput(gameObject, 0);
        oi.gameObject.transform.localPosition = new Vector3(0f, -70f, -99f);
        oi.gameObject.transform.localScale = new Vector3(800f, 800f, 1f);
		gameObject.tag = "Lamp";
    }

    public void MakeInitialConnections()
    {
        //Pour le moment la lampe ne peut se connecter à rien.
        return;
    }

    public void notify()
    {
        power = oi.value;

        if (!power)
        {
            image.sprite = textureOff;
        }
        else
        {
            image.sprite = textureOn;
            if (popup && enabled)
            {
                activatePopUp();
                enabled = false;
            }
        }
    }

    /// <summary>
    /// On change la texture de la lampe si elle reçoit ou pas du courant. Si la lampe a une fenetre de victoire assignee on arrete alors le rafraichissement.
    /// </summary>
   /* void Update()
    {
        notify();
    }*/

    public void PlaceOnGrid()
    {
        GridUtil.TakeNearestAnchor(gameObject);
    }

    /// <summary>
    /// On affiche la fenetre de victoire qui est desactivee au lancement du niveau. On lance le son correspondant a la reussite du niveau et si le niveau a ete fini pour la premiere fois on debloque le suivant.
    /// </summary>
    public void activatePopUp()
    {
        
        popup.SetActive(true);
        popup.transform.position = new Vector3(0,0, 1);
        SoundUtil.PlaySound("Sounds/victoire");
        string sceneName = SceneManager.GetActiveScene().name;
        double numScene = Char.GetNumericValue(sceneName, sceneName.Length - 1) + 1;
        if (ParametersLoader.GetUnlockedLevels() < numScene)
        {
            ParametersLoader.UnlockLevel();
            Debug.Log("niveau "+numScene+" debloqué");
        }
        else { Debug.Log("niveau"+numScene+" déja debloqué "+ ParametersLoader.GetUnlockedLevels()); }
    }

   
}

using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

/// <summary>
/// Permet de changer l'apparence de la lampe en fonction du courant et d'afficher la fenetre de victoire si elle est allumee.
/// </summary>
public class Lamp : MonoBehaviour, DevObjInit, Notifiable
{
    /// <summary>
    /// Pour gerer l'apparence de la lampe
    /// </summary>
    Image image;
    /// <summary>
    ///ed. L'image de la lampe alumée
    /// </summary>
    public Sprite textureOn;
    /// <summary>
    ///ed. L'image de la lampe éteinte.
    /// </summary>
    public Sprite textureOff;

    /// <summary>
    /// L'input de la lampe
    /// </summary>
    Obj_Input input;
    /// <summary>
    /// Si la lampe reçois du courant
    /// </summary>
    public bool power = false;

    /// <summary>
    /// Le pop up de victoire.
    /// </summary>
    public GameObject popup;

    public void Instantiate()
    {
        image = GetComponent<Image>();
        input = Obj_Input.createInput(gameObject, 0);
        input.gameObject.transform.localPosition = new Vector3(0f, -70f, -99f);
        input.gameObject.transform.localScale = new Vector3(800f, 800f, 1f);
		gameObject.tag = "Lamp";
    }

    public void MakeInitialConnections()
    {
        //Pour le moment la lampe ne peut se connecter à rien.
        return;
    }

    //En cas de changement de valeur on change l'etat de la lampe et on affiche le pop-up de victoire
    public void notify()
    {
        power = input.value;

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

    public void PlaceOnGrid()
    {
        GridUtil.TakeNearestAnchor(gameObject);
    }

    /// <summary>
    /// On affiche la fenetre de victoire qui est desactivee au lancement du niveau. 
    /// On lance le son correspondant a la reussite du niveau et si le niveau a ete fini pour la premiere fois on debloque le suivant.
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

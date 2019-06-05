using UnityEngine.UI;
using UnityEngine;
using System.Collections;

/// <summary>
/// Permet de changer l'apparence de la lampe en fonction du courant et d'afficher la fenetre de victoire si elle est allumee.
/// </summary>
public class Lamp : MonoBehaviour, IDevObjInit, INotifiable
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
    /// ed. Le pop up de victoire.
    /// </summary>
    public GameObject popup;


    private Obj_Input input;
    private bool power = false;



    /// <summary>
    /// L'effet de lumiere.
    /// </summary>
    public GameObject effect;

    /// <summary>
    /// Place la lampe et crée son input
    /// </summary>
    public void Instantiate()
    {
        image = GetComponent<Image>();
        input = Obj_Input.CreateInput(gameObject, 0);
        input.gameObject.transform.localPosition = new Vector3(0f, -65f, 1f);
        Vector3 v = input.transform.localScale;
        input.gameObject.transform.localScale = new Vector3(v.x * 20f, v.y * 25f, 1);

    }

    /// <summary>
    /// Ajoute le tag Lamp à la lampe
    /// </summary>
    public void TagGameObject()
    {
        gameObject.tag = "Lamp";
    }

    /// <summary>
    /// Inutilisée
    /// </summary>
    public void MakeInitialConnections()
    {
        //Pour le moment la lampe ne peut se connecter à rien.
        return;
    }

    /// <summary>
    /// Si la lampe s'allume on affiche le pop up
    /// </summary>
    public void notify()
    {
        power = input.value;

        if (!power)
        {
            image.sprite = textureOff;
            effect.SetActive(false);
            DesactivatePopUp();
        }
        else
        {
            effect.SetActive(true);
            SoundUtil.PlaySound("Sounds/light2");
            if (popup)
            {
                StartCoroutine(waiter());
            }
        }
    }

    /// <summary>
    /// Place la lampe sur la grille
    /// </summary>
    public void PlaceOnGrid()
    {
        GridUtil.TakeNearestAnchor(gameObject);
    }

    /// <summary>
    /// On affiche la fenetre de victoire qui est desactivee au lancement du niveau. 
    /// On lance le son correspondant a la reussite du niveau et si le niveau a ete fini pour la premiere fois on debloque le suivant.
    /// </summary>
    public void ActivatePopUp()
    {
        popup.SetActive(true);
        SetStateOfAllColliders(false);

        popup.transform.position = new Vector3(0, 0, 1);

        double numScene = ButtonUtil.GetLevelNumber();

        if (ParametersLoader.GetUnlockedLevels() <= numScene)
        {
            ParametersLoader.UnlockLevel();
        }
    }

    /// <summary>
    /// Inutilisée
    /// </summary>
    public void DesactivatePopUp()
    {
    }

    //fonction qui attend 1 seconde avant d'afficher la popup de victoire 
    IEnumerator waiter()
    {
        yield return new WaitForSecondsRealtime(2);
        if (power)
        {
            SoundUtil.PlaySound("Sounds/victoire");
            ActivatePopUp();
        }
        else
        {
            SoundUtil.PlaySound("Sounds/off");
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


}

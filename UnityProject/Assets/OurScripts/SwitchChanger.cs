using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// Permet de changer l'etat et la texture de l'interrupteur.
/// </summary>
public class SwitchChanger : MonoBehaviour, DevObjInit
{
    /// <summary>
    /// ed. Pour creer une connection à une autre porte.
    /// </summary>
    public GameObject initialConnection = null;
    /// <summary>
    /// ed. L'input auquel se connecter.
    /// </summary>
    public int inputNumber = 0;

    /// <summary>
    /// ed. Pour changer l'apparence du switch.
    /// </summary>
    public Sprite textureOn;
    /// <summary>
    /// ed. Pour changer l'apparence du switch.
    /// </summary>
    public Sprite textureOff;

    /// <summary>
    /// L'etat du switch
    /// </summary>
    private bool power = false;
    /// <summary>
    /// Pour changer la texture du switch
    /// </summary>
    private Image image;
    /// <summary>
    /// L'output du switch
    /// </summary>
    private Obj_Output output;

    
    /// <summary>
    /// Crée un input pour le switch et le connecte si il faut.
    /// </summary>
    public void Instantiate()
    {
        image = GetComponent<Image>();
        if (initialConnection == null)
        {
            output = Obj_Output.createOutput(gameObject, 0, true);
        } else
        {
            output = Obj_Output.createOutput(gameObject, 0, false);
        }

        output.gameObject.transform.localPosition = new Vector3(0f, 60f, -99f);
        Vector3 v = output.transform.localScale;
        output.transform.localScale = new Vector3(v.x * 50f, v.y * 50f, 1);
    }

    /// <summary>
    /// On se connecte à une portes placée par les Dev
    /// </summary>
    public void MakeInitialConnections()
    {
         if( initialConnection != null)
        {
            Obj_Output.TryToConnect(output, initialConnection, inputNumber);
        }
    }

    /// <summary>
    /// Place le switch sur la grille
    /// </summary>
    public void PlaceOnGrid()
    {
        GridUtil.TakeNearestAnchor(gameObject);
        gameObject.tag = "Switch";
    }


    /// <summary>
    /// Change la texture du switch lorsque le bouton est presse.
    /// </summary>
    public void OnMouseDown()
    {
        ChangeTexture();
    }

    /// <summary>
    /// Inverse l'etat du courant et joue le son correspondant lors du relachement du switch.
    /// </summary>
    public void OnMouseUp()
    {
        output.value = power;
        SoundUtil.PlaySound("Sounds/switch");
        output.notify();
    }

    /// <summary>
    /// Change la texture de l'interrupteur pour un retour visuel de son activation.
    /// </summary>
    private void ChangeTexture()
    {
        Debug.Log("Switch "+gameObject);
        if (!power)
        {
            image.sprite = textureOn;
            power = true;
        }
        else
        {
            image.sprite = textureOff;
            power = false;
        }
    }
}

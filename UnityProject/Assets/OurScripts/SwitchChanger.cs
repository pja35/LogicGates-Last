using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// Permet de changer l'etat et la texture de l'interrupteur.
/// </summary>
public class SwitchChanger : MonoBehaviour, DevObjInit
{
    //ed. Pour creer une connection à une autre porte.
    public GameObject initialConnection = null;
    //ed. L'input auquel se connecter.
    public int inputNumber = 0;

    //ed. Pour changer l'apparence du switch.
    public Sprite textureOn;
    public Sprite textureOff;

    private bool power = false;
    private Image image;
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

    public void MakeInitialConnections()
    {
         if( initialConnection != null)
        {
            Obj_Output.TryToConnect(output, initialConnection, inputNumber);
        }
    }

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

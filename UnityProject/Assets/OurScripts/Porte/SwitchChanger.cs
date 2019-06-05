using UnityEngine.UI;
using UnityEngine;
using System;

/// <summary>
/// Permet de changer l'etat et la texture de l'interrupteur.
/// </summary>
public class SwitchChanger : MonoBehaviour, IDevObjInit
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


    private bool powered = false;
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
            output = Obj_Output.CreateDisconactableOutput(gameObject, 0);
        }
        else
        {
            output = Obj_Output.CreateUndisconactableOutput(gameObject, 0);
        }

        output.gameObject.transform.localPosition = new Vector3(0f, 45f, 1f);
        Vector3 v = output.transform.localScale;
        output.transform.localScale = new Vector3(v.x * 20f, v.y * 25f, 1);
    }

    /// <summary>
    /// On se connecte à une portes placée par les Dev
    /// </summary>
    public void MakeInitialConnections()
    {
        if (initialConnection != null)
        {
            Obj_Output.TryToConnect(output, initialConnection, inputNumber);
        }
    }

    /// <summary>
    /// Tag l'objet avec le tag Switch
    /// </summary>
    public void TagGameObject()
    {
        gameObject.tag = "Switch";
    }

    /// <summary>
    /// Place le switch sur la grille
    /// </summary>
    public void PlaceOnGrid()
    {
        GridUtil.TakeNearestAnchor(gameObject);
    }


    /// <summary>
    /// Change la texture du switch lorsqu'on lui clique dessus.
    /// </summary>
    public void OnMouseDown()
    {
        if (ParametersLoader.GetSnooze())
        {
            Handheld.Vibrate();
        }
           
        powered = !powered;
        ChangeTexture();
        SoundUtil.PlaySound("Sounds/switch");
        output.value = powered;
        output.notify();
    }

    private void ChangeTexture()
    {
        image.sprite = (powered) ? textureOn : textureOff;
    }
}

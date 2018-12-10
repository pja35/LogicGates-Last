using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fils : MonoBehaviour, Notifiable
{
    /// <summary>
    /// L'objet de départ du fils (porte le line renderer).
    /// </summary>
    private GameObject start;
    /// <summary>
    /// L'objet de fin du fils.
    /// </summary>
    private GameObject end;
    /// <summary>
    /// La valeur de l'entrée
    /// </summary>
    private bool start_val;
    /// <summary>
    /// La valeur du fil
    /// </summary>
    public bool powered = false;


    /// <summary>
    /// On se connecte aux I/O on instantie la valeur du fil et on notifie
    /// </summary>
    /// <param name="end">La connection de fin.</param>
    public void Init_Fils(GameObject end)
    {
        this.start = gameObject;
        this.end = end;
        start_val = start.GetComponent<Obj_Output>().value;
        end.GetComponent<Obj_Input>().connection = this;

        DrawUtil.DrawFil(start, end);

        notify();
    }

    /// <summary>
    /// En cas de changement de l'entrée le fil actualise la valeur de sa sortie et change sa couleur.
    /// </summary>
    public void notify()
    {
        Debug.Log("Fils " + gameObject + " to " + end.gameObject);
        start_val = start.GetComponent<Obj_Output>().value;
        if (powered != start_val)
        {
            powered = start_val;
            end.GetComponent<Obj_Input>().setValue(powered);
        }

        if (powered)
        {
            DrawUtil.SetLineColor(gameObject, Color.green);
        }
        else
        {
            DrawUtil.SetLineColor(gameObject, Color.red);
        }

        DrawUtil.UpdateFil(start, end);
        end.GetComponent<Notifiable>().notify();

    }

    //! Détruit le fil créé et supprime 
    public void Destroy_Fils()
    {
        end.GetComponent<Obj_Input>().value = false;
        DrawUtil.EraseLine(gameObject);
        Destroy(this);
    }
}

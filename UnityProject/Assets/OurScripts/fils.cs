using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fils : MonoBehaviour, Notifiable
{
    private GameObject start;
    private GameObject end;
    private bool start_val;
    public bool powered = false;
    LineRenderer lineRenderer;

    public void Init_Fils(GameObject end)
    {
        this.start = gameObject;
        this.end = end;
        start_val = start.GetComponent<Obj_Output>().value;
        end.GetComponent<Obj_Input>().connection = this;

        lineRenderer = DrawUtil.DrawLine(start, end);

        notify();
    }

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

        DrawUtil.UpdateLine(start, end, lineRenderer);
        end.GetComponent<Notifiable>().notify();

    }

    //! Détruit le fil créé et supprime 
    public void Destroy_Fils()
    {
        end.GetComponent<Obj_Input>().value = false;
        DrawUtil.EraseLine(gameObject);
        Destroy(this);
    }
    /*
    void Update()
    {
        notify();
    }*/

    // Update is called once per frame
    // La fin de Update est a modifié lorsque l'on fera des fils qui suivent la grille pour que ça soit propre.
   /* void Update()
    {
        notify();*/
        /*
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

        DrawUtil.UpdateLine(start, end, lineRenderer);
        */

    //}
}

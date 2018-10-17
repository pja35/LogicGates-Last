using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragger : MonoBehaviour
{
    public Vector3 initialObjMouseDistance;
    public bool mouseDown = false;


    /*Find the nearest anchor in the list form the given object */

    public Vector3 FindNearest(List<Vector3> anchor_list, GameObject to_move)
    {

        Vector3 to_move_pos = to_move.transform.position;
        Vector3 nearest = anchor_list[0];
        float distance = Vector3.Distance(nearest, to_move_pos);

        foreach (Vector3 act in anchor_list)
        {
            float dist_act = Vector3.Distance(act, to_move_pos);
            if (dist_act < distance)
            {
                distance = dist_act;
                nearest = act;
            }
        }
        return nearest;
    }

    public void OnMouseDown()
    {
        mouseDown = true;
        /*To avoid the object going right under the mouse cursor*/
        initialObjMouseDistance = gameObject.transform.position
        - Camera.main.ScreenToWorldPoint(Input.mousePosition);

    }

    public void OnMouseUp()
    {
        mouseDown = false;
        /*Place the object on the nearest anchor*/
        GameObject grid_holder = GameObject.Find("GridHolder");
        Grid grid = grid_holder.GetComponent<Grid>();
        gameObject.transform.position = FindNearest(grid.anchor_list, gameObject);
    }

    /*if the mouse is pressed the object follow the mouse pointer*/
    public void Update()
    {
        if (mouseDown)
        {
            gameObject.transform.position =
                Camera.main.ScreenToWorldPoint(Input.mousePosition)
                + initialObjMouseDistance;
        }
    }
}

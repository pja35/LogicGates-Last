using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggerInstantiater : Dragger
{
    private GameObject cloned = null;

    private new void OnMouseDown()
    {
        mouseDown = true;
        /*To avoid the object going right under the mouse cursor*/
        initialObjMouseDistance = gameObject.transform.position
            - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        /*Create a new object wich cant't multiply and will follow the corsor*/
        cloned = Instantiate(gameObject);
        cloned.transform.SetParent(gameObject.transform.parent);
        cloned.transform.localScale = gameObject.transform.localScale;
        Destroy(cloned.GetComponent<DraggerInstantiater>());

        cloned.AddComponent<Dragger>();

        /* if (cloned.GetComponent<Dragger>().anchorPoint != null)
         {
             Debug.Log("Pas nul");
             cloned.GetComponent<Dragger>().anchorPoint.free = true;
             cloned.GetComponent<Dragger>().anchorPoint = null;
         }*/

    }

    private new void OnMouseUp()
    {
        mouseDown = false;
        GameObject grid_holder = GameObject.Find("GridHolder");
        GridCreater grid = grid_holder.GetComponent<GridCreater>();
        AnchorState nearest = FindNearest(grid.anchor_list, cloned);
        if (nearest == null)
        {
            Destroy(cloned);
        }
        else
        {
            DestroyOnBin(cloned);
            cloned.GetComponent<Dragger>().anchorPoint = nearest;
            cloned.GetComponent<Dragger>().anchorPoint.free = false;
            cloned.transform.transform.position = nearest.position;
        }
    }

    private new void Update()
    {
        if (mouseDown)
        {
            cloned.transform.position =
                Camera.main.ScreenToWorldPoint(Input.mousePosition)
                + initialObjMouseDistance;
        }
    }




}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggerInstantiater : Dragger
{
    private GameObject cloned;

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

    }

    private new void OnMouseUp()
    {
        mouseDown = false;
        GameObject grid_holder = GameObject.Find("GridHolder");
        Grid grid = grid_holder.GetComponent<Grid>();
        cloned.transform.transform.position = FindNearest(grid.anchor_list, cloned);
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

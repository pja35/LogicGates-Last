using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! Objet de base permettant de créer un dragger.
public class DraggerInstantiater : Dragger
{
    private GameObject cloned = null;

	/**
		Créer un clone de la porte actuelle que l'on pourra déplacer.
	*/
    private new void OnMouseDown()
    {
        mouseDown = true;
        //To avoid the object going right under the mouse cursor
        initialObjMouseDistance = gameObject.transform.position
            - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Create a new object wich cant't multiply and will follow the cursor
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

	/**
		Gère le placement de l'objet quand il est reposé.
	*/
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

	/**
		Gère le déplacement de l'objet quand il n'est pas fixé.
	*/
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

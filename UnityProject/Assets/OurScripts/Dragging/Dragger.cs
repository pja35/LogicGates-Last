using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! Gestionnaire des déplacements et positionnement d'une porte.
public class Dragger : MonoBehaviour
{	
	/** 
		\brief Conserve la distance au centre de l'objet.
		Exemple: Si on clique dans un angle du carré le centre garde la distance qu'il a avec le pointeur.
	*/
    public Vector3 initialObjMouseDistance;
	//! Booléen indiquant l'état de la souris
    public bool mouseDown = false;
	
	//! Ancre à laquel le dragger est attaché.
    public AnchorState anchorPoint = null;


    /** 
		Trouve l'ancre la plus proche dans la liste qui lui est passée.
		\param anchor_list La liste des ancres à tester.
		\param to_move L'objet qui servira de point de référence pour la distance
		\warning Cette fonction renvoie null si elle ne trouve pas d'ancre  		
	*/
    public AnchorState FindNearest(List<AnchorState> anchor_list, GameObject to_move)
    {

        Vector3 to_move_pos = to_move.transform.position;
        AnchorState nearest = anchor_list[0];
        float distance = float.MaxValue;

        foreach (AnchorState act in anchor_list)
        {
            float dist_act = Vector3.Distance(act.position, to_move_pos);
            if (dist_act < distance && act.free)
            {
                distance = dist_act;
                nearest = act;
            }
        }

        return (nearest.free) ? nearest : null;
    }

	//! Fonction qui va détruire l'objet qui lui est passée en argument.
    public void DestroyOnBin(GameObject obj)
    {
        GameObject bin = GameObject.Find("Bin");
        Vector3 binPos = bin.transform.localPosition;
        Vector3 binSize = bin.transform.localScale;
        Debug.Log(binSize.ToString());

        Vector3 objPos = obj.transform.localPosition;

        if ((objPos.x > binPos.x - binSize.x)
            && (objPos.x < binPos.x + binSize.x)
            && (objPos.y < binPos.y + binSize.y)
            && (objPos.y > binPos.y - binSize.y))
        {
            Destroy(obj);
        }
    }

	
	//! Permet à l'objet d'être déplacé.
    public void OnMouseDown()
    {
        mouseDown = true;
		
        // To avoid the object going right under the mouse cursor
        initialObjMouseDistance = gameObject.transform.position
        - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (anchorPoint != null)
        {
            anchorPoint.free = true;
            anchorPoint = null;
        }

    }

	//! Essaie de placer l'objet sur l'ancre la plus proche, si elle n'existe pas l'objet est détruit. 
    public void OnMouseUp()
    {
        mouseDown = false;
		
        //Place the object on the nearest anchor
        GameObject grid_holder = GameObject.Find("GridHolder");
        GridCreater grid = grid_holder.GetComponent<GridCreater>();
        AnchorState nearestAnchor = FindNearest(grid.anchor_list, gameObject);
        if (nearestAnchor == null)
        {
            Destroy(gameObject);

        }
        else
        {
            DestroyOnBin(gameObject);
            anchorPoint = nearestAnchor;
            anchorPoint.free = false;
            gameObject.transform.position = nearestAnchor.position;
        }
    }

    //! Permet à cet objet d'être déplacé par le curseur
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragger : MonoBehaviour
{
    public Vector3 initialObjMouseDistance;
    public bool mouseDown = false;

    public AnchorState anchorPoint = null;


    /*Find the nearest anchor in the list form the given object */

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

    public void OnMouseDown()
    {
        mouseDown = true;
        /*To avoid the object going right under the mouse cursor*/
        initialObjMouseDistance = gameObject.transform.position
        - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (anchorPoint != null)
        {
            anchorPoint.free = true;
            anchorPoint = null;
        }

    }

    public void OnMouseUp()
    {
        mouseDown = false;
        /*Place the object on the nearest anchor*/
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

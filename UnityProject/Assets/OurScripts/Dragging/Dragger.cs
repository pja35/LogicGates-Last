using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Permet de déplacer un objet à la souris. L'objet adère aux ancres de l'écran.
/// </summary>
public class Dragger : MonoBehaviour
{
    /// <summary>
    /// Pour eviter que l'objet se positione juste en dessous de la souris.
    /// </summary>
    public Vector3 initialObjMouseDistance;
    /// <summary>
    ///  Booléen indiquant l'état de la souris
    /// </summary>
    public bool mouseDown = false;
    /// <summary>
    /// Ancre à laquel le dragger est attaché.
    /// </summary>
    private AnchorState anchorPoint = null;




    /// <summary>
    /// Destruit obj si il entre en contact avec la poubelle
    /// </summary>
    /// <param name="obj">L'objet à détruire.</param>
    public bool DestroyedOnBin(GameObject obj)
    {
        GameObject bin = GameObject.Find("Bin");
        Vector3 binPos = bin.transform.position;
        Vector3 binSize = bin.transform.lossyScale;

        Vector3 objPos = obj.transform.position;

        //Si on est dans la corbeille.
        if ((objPos.x > binPos.x - binSize.x)
            && (objPos.x < binPos.x + binSize.x)
            && (objPos.y < binPos.y + binSize.y)
            && (objPos.y > binPos.y - binSize.y))
        {
            gameObject.GetComponent<Gate>().Destroy();
            return true;
        }
        return false;
    }


    /// <summary>
    /// Déplace l'objet sur un appui de la souris.
    /// </summary>
    public void OnMouseDown()
    {
        mouseDown = true;
        //Défixe tous les objets mouvants de l'objet cliqué
        Component[] fixables = (gameObject.GetComponents(typeof(Fixable)));
        foreach (Fixable f in fixables)
        {
            f.OnUnfix();
        }
        // Pour eviter que l'objet ne se place pile sous le curseur.
        initialObjMouseDistance = gameObject.transform.position
        - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (anchorPoint != null)
        {
            anchorPoint.free = true;
            anchorPoint = null;
        }

    }

    /// <summary>
    /// Tente de placer l'objet sur la grille et le détruit si on ne peut pas.
    /// </summary>
    public void OnMouseUp()
    {
        mouseDown = false;

        //Récupère l'ancre libre la plus proche.
        GameObject grid_holder = GameObject.Find("GridHolder");
        GridCreater grid = grid_holder.GetComponent<GridCreater>();
        AnchorState nearestAnchor = GridUtil.FindNearestFreeAnchor(grid.anchor_list, gameObject);

        if (nearestAnchor == null){ //Si on ne trouve pas d'ancre.
            Destroy(gameObject);
        }else if (DestroyedOnBin(gameObject)) {//Si la porte etait au dessus de la pubelle
            return;
        }else {//Prend l'ancre et place la porte dessus.
            GridUtil.TakeAnchor(nearestAnchor, gameObject);
            anchorPoint = nearestAnchor;
            Component[] fixables = (gameObject.GetComponents(typeof(Fixable)));
            foreach (Fixable f in fixables)
            {
                f.OnFix();
            }
        }
        SoundUtil.PlaySound("Sounds/clic");
    }

    /// <summary>
    /// L'objet suivra le curseur si il est cliqué.
    /// </summary>
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

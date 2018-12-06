using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Détaille les informations sur une ancre.
/// </summary>
public class AnchorState
{
    //! Si l'ancre est libre ou non.
    public bool free;
    //! Position de l'ancre
    private Vector3 position;
    //! Position de l'ancre dans la matrice d'ancres
    private Vector2 grid_pos;

    /// <summary>
    /// Crée une ancre
    /// </summary>
    /// <param name="position">La position absolue de l'ancre.</param>
    /// <param name="grid_pos">La position de l'ancre dans la grille.</param>
    public AnchorState(Vector3 position, Vector2 grid_pos, bool free)
    {
        this.position = position;
        this.grid_pos = grid_pos;
        this.free = free;
    }

    public Vector3 GetPosition()
    {
        return position;
    }

    public Vector2 GetGridPos()
    {
        return grid_pos;
    }
}

public class GridUtil : MonoBehaviour
{


    /// <summary>
    /// Trouve l'ancre libre la plus proche de to_move.
    /// </summary>
    /// <param name="anchorList">La liste des ancres.</param>
    /// <param name="toMove">L'objet donnant la distance.</param>
    /// <returns>L'ancre libre la plus proche. Ou null si aucune n'est libre.</returns>
    public static AnchorState FindNearestFreeAnchor(List<AnchorState> anchorList, GameObject toMove)
    {

        Vector3 toMovePos = toMove.transform.position;
        //On initialise avec le premier element pour faciliet le calcul.
        AnchorState nearest = anchorList[0];
        float distance = float.MaxValue;

        foreach (AnchorState act in anchorList)
        {
            float dist_act = Vector3.Distance(act.GetPosition(), toMovePos);
            if (dist_act < distance && act.free)
            {
                distance = dist_act;
                nearest = act;
            }
        }
        //si aucune ancre libre n'as été trouvée.
        return (nearest.free) ? nearest : null;
    }
	

	
    /// <summary>
    /// Récupère la liste des ancres du de GridHolder et trouve la plus proche de toMove.
    /// </summary>
    /// <param name="toMove">L'objet donnant la distance.</param>
    /// <returns>L'ancre la plus proche.</returns>
    public static AnchorState FindNearsetAnchor(GameObject toMove)
    {
        List<AnchorState> anchors = GameObject.Find("GridHolder").GetComponent<GridCreater>().anchor_list;
        return FindNearesttAnchor(anchors, toMove);
    }


    /// <summary>
    /// Trouve l'ancre la plus proche de to_move.
    /// </summary>
    /// <param name="anchor_list">La liste des ancres.</param>
    /// <param name="to_move">L'objet donnant la distance.</param>
    /// <returns>L'ancre prise.</returns>
    public static AnchorState FindNearesttAnchor(List<AnchorState> anchor_list, GameObject to_move)
    {
        Vector3 to_move_pos = to_move.transform.position;
        AnchorState nearest = anchor_list[0];
        float distance = -1;

        foreach (AnchorState act in anchor_list)
        {
            float dist_act = Vector3.Distance(act.GetPosition(), to_move_pos);
            if (dist_act < distance || distance == -1)
            {
                distance = dist_act;
                nearest = act;
            }
        }

        return nearest;
    }

    /// <summary>
    ///Prend une ancre et place le preneur dessus.
    /// Cette fonction doit être utilisée avec des objets inamovibles.
    /// </summary>
    /// <param name="anchor">L'ancre à prendre.</param>
    /// <param name="taker">L'objet prenant l'ancre.</param>
    public static void TakeAnchor(AnchorState anchor, GameObject taker)
    {
        anchor.free = false;
        taker.transform.position = anchor.GetPosition();
    }

    /// <summary>
    /// Prend l'ancre la plus proche de l'objet qe celle-ci soit libre ou non.
    /// </summary>
    /// <param name="taker">L'objet prenant une ancre.</param>
    /// <returns>L'ancre prise.</returns>
   public static AnchorState TakeNearestAnchor(GameObject taker)
    {
        AnchorState choosen = GridUtil.FindNearsetAnchor(taker);
        GridUtil.TakeAnchor(choosen, taker);
        return choosen;
    }
    
}
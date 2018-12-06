using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Une clase pour gerer les emplacements de portes.
/// </summary>
public class EmplacementPorte : MonoBehaviour, DevObjInit
{
    public void Instantiate()
    {
        return;
    }

    public void MakeInitialConnections()
    {
        return;
    }

    //Fait aussi le trvail de Instantiate.
    public void PlaceOnGrid()
    {
        AnchorState choosen =  GridUtil.TakeNearestAnchor(gameObject);

        if (GameObject.Find("GridHolder").GetComponent<GridCreater>().initialState) {
            Debug.LogWarning("Vous essayez d'utiliser un emplacement de porte alors que l'etat initial de la grille n'est pas à faux." +
                " Le comportement normal des emplacement ne pourra pas avoir lieu.");
        }

        //On rajoute une ancre à la grille sur laquelle les portes pouront se fixer. Normalement ces ancres seront les seules disponibles.
        List<AnchorState> gridAnchors = GameObject.Find("GridHolder").GetComponent<GridCreater>().anchor_list;
        gridAnchors.Add(new AnchorState(choosen.GetPosition(), choosen.GetGridPos(), true));
    }

}

using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Un ensemble de fonction pour faciliter l'utilisation de la grille d'ancres
/// </summary>
public class GridUtil : MonoBehaviour
{

    /// <summary>
    /// Prend l'ancre la plus proche de l'objet qe celle-ci soit libre ou non.
    /// </summary>
    /// <param name="taker">L'objet prenant une ancre.</param>
    /// <returns>L'ancre prise.</returns>
    public static IAnchor TakeNearestAnchor(GameObject taker)
    {
        IAnchor choosen = GridUtil.FindNearestAnchor(taker);
        choosen.TakeAnchor(taker);
        return choosen;
    }

    /// <summary>
    /// Récupère la liste des ancres du de GridHolder et trouve la plus proche de from.
    /// </summary>
    /// <param name="from">L'objet donnant la distance.</param>
    /// <returns>L'ancre la plus proche.</returns>
    public static IAnchor FindNearestAnchor(GameObject from)
    {
        return FindNearestAnchor(from.transform.position);
    }

    /// <summary>
    /// Récupère la liste des ancres du de GridHolder et trouve la plus proche de from.
    /// </summary>
    /// <param name="from">L'objet donnant la distance.</param>
    /// <returns>L'ancre la plus proche.</returns>
    public static IAnchor FindNearestAnchor(Vector3 from)
    {
        IAnchor nearestFromMat = NearestAnchorOfAnchorMat(from);
        float distanceForMat = Vector3.Distance(nearestFromMat.GetPosition(), from);

        List<IAnchor> additionalAnchors = GameObject.Find("GridHolder").GetComponent<GridCreater>().GetAdditionalAnchors();
        if(additionalAnchors.Count != 0)
        {
            IAnchor nearestFromAdditional = FindNearestAnchor(additionalAnchors, from);
            float distanceForAdditional = Vector3.Distance(nearestFromAdditional.GetPosition(), from);  
            return (distanceForAdditional < distanceForMat) ? nearestFromAdditional : nearestFromMat;

        } else
        {
            return nearestFromMat;
        }

    }

    /// <summary>
    /// Trouve l'ancre la plus proche que l'objet peut prendre
    /// </summary>
    /// <param name="from">L'objet à placer (doit avoir un comportement de porte attaché)</param>
    /// <returns></returns>
    public static IAnchor FindNearestPlacableAnchor(GameObject from)
    {
        GridCreater grid = GameObject.Find("GridHolder").GetComponent<GridCreater>();
        // On donne la priorité aux ancres additionelles
        if (! grid.anchorsCanBeTaken)
        {
            return FindNearestAnchor(grid.GetAdditionalAnchors(), from.transform.position);
        }
        else return FindNearestAnchor(from.transform.position);
    }


    /// <summary>
    /// Trouve l'ancre la plus proche de to_move.
    /// </summary>
    /// <param name="anchor_list">La liste des ancres.</param>
    /// <param name="from">L'objet donnant la distance.</param>
    /// <returns>L'ancre prise.</returns>
    public static IAnchor FindNearestAnchor(List<IAnchor> anchor_list, Vector3 from)
    {
        IAnchor nearest = anchor_list[0];
        float distanceTo = -1;

        foreach (IAnchor AnchorAct in anchor_list)
        {
            float distAct = Vector3.Distance(AnchorAct.GetPosition(), from);
            if (distAct < distanceTo || distanceTo == -1)
            {
                distanceTo = distAct;
                nearest = AnchorAct;
            }
        }

        return nearest;
    }



    private static int GetNearestXCoordinate(Vector3 from, IAnchor[,] anchorMat)
    {
        int nearestAnchorXcoord = 0;
        float bestDistance = Vector3.Distance(from, anchorMat[0, 0].GetPosition());

        for (int i = 0; i < anchorMat.GetLength(0); i++)
        {
            float actualDistance = Vector3.Distance(from, anchorMat[i, 0].GetPosition());
            if (actualDistance < bestDistance)
            {
                nearestAnchorXcoord = i;
                bestDistance = actualDistance;
            }
        }
        return nearestAnchorXcoord;
    }

    private static int GetNearestYCoordinate(Vector3 from, IAnchor[,] anchorMat)
    {
        int nearestAnchorYcoord = 0;
        float bestDistance = Vector3.Distance(from, anchorMat[0, 0].GetPosition());

        for (int i = 0; i < anchorMat.GetLength(1); i++)
        {
            float actualDistance = Vector3.Distance(from, anchorMat[0, i].GetPosition());
            if (actualDistance < bestDistance)
            {
                nearestAnchorYcoord = i;
                bestDistance = actualDistance;
            }
        }
        return nearestAnchorYcoord;
    }

    /// <summary>
    /// Renvoi l'ancre la plus proche parmis une matrice d'ancres
    /// </summary>
    /// <param name="from">L'objet de référence poiur la distance</param>
    /// <param name="anchorMat">La matrice des ancres</param>
    /// <returns>L'ancre la plus proche</returns>
    public static IAnchor NearestAnchorOfAnchorMat(Vector3 from, IAnchor[,] anchorMat)
    {
        int xCoordinate = GetNearestXCoordinate(from, anchorMat);
        int yCoordiante = GetNearestYCoordinate(from, anchorMat);

        return anchorMat[xCoordinate, yCoordiante];
    }

    /// <summary>
    /// Retourne l'ancre la plus proche parmis celles de la grille
    /// </summary>
    /// <param name="from">L'objet de référence poiur la distance</param>
    /// <returns>L'ancre la plus proche</returns>
    public static IAnchor NearestAnchorOfAnchorMat(Vector3 from)
    {
       IAnchor[,] anchors = GameObject.Find("GridHolder").GetComponent<GridCreater>().GetMatAnchor();
       return NearestAnchorOfAnchorMat(from, anchors);
    }

    private static bool CheckCollision(IAnchor anchor, Gate gate)
    {
        IAnchor[,] anchorsMat = GameObject.Find("GridHolder").GetComponent<GridCreater>().GetMatAnchor();
        Vector2 matPosition = anchor.GetGridPos();
        int sizeX = gate.GetAnchorsX() / 2;
        int sizeY = (gate.GetAnchorsY() + gate.GetSpaceForIO())  / 2;
        for (int i = (int)matPosition.x - sizeX; i <= matPosition.x + sizeX; i++)
        {
            for (int j = (int)matPosition.y - sizeY;  j <= matPosition.y + sizeY; j++)
            {
                if (!anchorsMat[i, j].IsWireFree())
                {
                    return true;
                }
            }
        }
        return false;
    }

    private static bool CheckCollisionWire(IAnchor anchor, Gate gate)
    {
        IAnchor[,] anchorsMat = GameObject.Find("GridHolder").GetComponent<GridCreater>().GetMatAnchor();
        Vector2 matPosition = anchor.GetGridPos();
        int sizeX = gate.GetAnchorsX() / 2;
        int sizeY = (gate.GetAnchorsY() + gate.GetSpaceForIO())  / 2;
        for (int i = (int)matPosition.x - sizeX; i <= matPosition.x + sizeX; i++)
        {
            for (int j = (int)matPosition.y - sizeY+1;  j <= matPosition.y + sizeY -1; j++)
            {
                if (!anchorsMat[i, j].IsFree())
                {
                    return true;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// Est ce que l'objet entre en collision avec un autre objet si il prend l'ancre donnée
    /// </summary>
    /// <param name="anchor">L'ancre à prendre</param>
    /// <param name="gate">La porte permettant de récuperer la taille de l'objet</param>
    public static bool IsColliding(IAnchor anchor,Gate gate)
    {
        if (anchor.HandleCollisions())
        {
            if(CheckCollision(anchor, gate)||CheckCollisionWire(anchor,gate))
                return true;
            else{
                return false;
            }
                
        } else
        {
            return !anchor.IsWireFree();
        }
            
    }

    /// <summary>
    /// Détécte si l'objet sort de la grille si il prend une ancre
    /// </summary>
    /// <param name="anchor">L'ancre à prendre</param>
    /// <param name="gate">La porte permettant de récuperer la taille de l'objet</param>
    public static bool IsOutOfBound(IAnchor anchor,Gate gate)
    {
        IAnchor[,] anchorsMat = GameObject.Find("GridHolder").GetComponent<GridCreater>().GetMatAnchor();
        Vector2 matPosition = anchor.GetGridPos();
        int sizeX = gate.GetAnchorsX() / 2;
        int sizeY = (gate.GetAnchorsY() + gate.GetSpaceForIO()) / 2;
        return (matPosition.x - sizeX < 0 ||
                matPosition.y - sizeY < 0 ||
                matPosition.x + sizeX >= anchorsMat.GetLength(0) ||
                matPosition.y + sizeY >= anchorsMat.GetLength(1));
    }

}
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

    private IAnchor anchorPoint = null;
    private Vector3 referencePointForCableUpdate;

    /// <summary>
    /// Déplace l'objet sur un appui de la souris.
    /// </summary>
    public void OnMouseDown()
    {
        mouseDown = true;
        //Défixe tous les objets mouvants de l'objet cliqué
        UnfixAllFixables();
        // Pour eviter que l'objet ne se place pile sous le curseur.
        initialObjMouseDistance = gameObject.transform.position
        - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (anchorPoint != null)
        {
            GridCreater grid = GameObject.Find("GridHolder").GetComponent<GridCreater>();
            Gate gate = gameObject.GetComponent<Gate>();
            grid.SetSurroundingAnchorsState(anchorPoint, gate, 1);
            anchorPoint.Untake(gameObject);
            anchorPoint = null;
        }

        referencePointForCableUpdate = gameObject.transform.position;

    }

    private void UnfixAllFixables()
    {
        IFixable[] fixables = gameObject.GetComponents<IFixable>();
        foreach (IFixable f in fixables)
        {
            f.OnUnfix();
        }
    }

    /// <summary>
    /// L'objet suivra le curseur si il est cliqué.
    /// </summary>
    public void Update()
    {
        Vector3 distBetweenAnchors = GridCreater.GetDistBtwnAnchors();

        if (mouseDown)
        {
            CheckInvalidPlacement();

            Gate gate = gameObject.GetComponent<Gate>();
            UpdateLineDraw(gate, distBetweenAnchors);

            gameObject.transform.position =
                Camera.main.ScreenToWorldPoint(Input.mousePosition)
                + initialObjMouseDistance;

        }
    }

    private void CheckInvalidPlacement()
    {
        IAnchor anchor = GridUtil.FindNearestPlacableAnchor(gameObject);
        Gate gate = gameObject.GetComponent<Gate>();
        if (!GridUtil.IsOutOfBound(anchor, gate) && !GridUtil.IsColliding(anchor, gate))
        {
            SpriteUtil.RemoveCross(gameObject);
        }
        else
        {
            SpriteUtil.AddCross(gameObject);
        }
    }

    private void UpdateLineDraw(Gate gate, Vector3 distBetweenAnchors)
    {
        int anchorsElapsed = AnchorsElapsed(distBetweenAnchors);
        if (anchorsElapsed == 1)
        {
            gate.UpdateEndOfLine();
        }
        else if (anchorsElapsed > 1)
        {
            referencePointForCableUpdate = gameObject.transform.position;
            gate.UpdateLineDraw();
        }
    }

    private int AnchorsElapsed(Vector3 distBetweenAnchors)
    {
        return (int)Mathf.Max(Mathf.Abs(gameObject.transform.position.x - referencePointForCableUpdate.x), (Mathf.Abs(gameObject.transform.position.y - referencePointForCableUpdate.y)));

    }

    /// <summary>
    /// Tente de placer l'objet sur la grille et le détruit si on ne peut pas.
    /// </summary>
    public void OnMouseUp()
    {
        mouseDown = false;

        //Récupère l'ancre libre la plus proche.
        IAnchor nearestAnchor = GridUtil.FindNearestPlacableAnchor(gameObject);
        Gate gate = gameObject.GetComponent<Gate>();

        if (PlacementIsInvalid(nearestAnchor, gate))
        { //Si on ne trouve pas d'ancre.
            gate.Destroy();
            Destroy(gameObject);
        }
        else
        {//Prend l'ancre et place la porte dessus.
            PlaceObject(nearestAnchor, gate);
            anchorPoint = nearestAnchor;
            SoundUtil.PlaySound("Sounds/clic");
        }
    }

    private bool PlacementIsInvalid(IAnchor anchor, Gate gate)
    {
        return (anchor == null
            || GridUtil.IsOutOfBound(anchor, gate)
            || !anchor.CanTake(gameObject));
    }

    private void PlaceObject(IAnchor anchor, Gate taker)
    {
        anchor.TakeAnchor(gameObject);
        FixAllFixables();
        anchor.FinishedTaking(gameObject);
        gameObject.GetComponent<Gate>().UpdateLineDraw();
    }

    private void FixAllFixables()
    {
        IFixable[] fixables = gameObject.GetComponents<IFixable>();
        foreach (IFixable f in fixables)
        {
            f.OnFix();
        }
    }
}

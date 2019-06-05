using UnityEngine;

public class CageAnchor : AnchorState, IAnchor
{
    private int MaxIn;
    private int MaxOut;

    /// <summary>
    /// Initialise l'ancre
    /// </summary>
    /// <param name="position">Position en jeu</param>
    /// <param name="grid_pos">Position dans la grille</param>
    /// <param name="free">L'ancre est elle libre</param>
    /// <param name="nbIn">Nombre d'entrées que les portes à placer devront avoir</param>
    /// <param name="nbOut">Nombre d'entrées que les portes à placer devront avoir</param>
    public void SetAnchor(Vector3 position, Vector2 grid_pos, int free,int nbIn, int nbOut)
    {
        base.SetAnchor(position, grid_pos, free);
        this.MaxIn = nbIn;
        this.MaxOut = nbOut;
    }

    /// <summary>
    /// Vérifie que la porte est disponible et as le bon nombre d'IO
    /// </summary>
    /// <param name="taker">Le preneur</param>
    public new bool CanTake(GameObject taker)
    {
        Gate gateTake = taker.GetComponent<Gate>();
        return (free == 1 && gateTake != null
            && gateTake.NbIn() == MaxIn && gateTake.NbOut() == MaxOut);
    }

    /// <summary>
    /// Place l'objet sur l'emplacement
    /// </summary>
    /// <param name="taker">L'objet à placer</param>
    public new void TakeAnchor(GameObject taker)
    {
        SendMessage("AnchorTaken", taker);
        base.TakeAnchor(taker);
        taker.transform.localPosition += new Vector3(0, 0, -1);
    }

    /// <summary>
    /// Libère l'emplacement et réactive les IO de la porte
    /// </summary>
    public new void Untake(GameObject untaker)
    {
        SendMessage("AnchorFreed");
        base.Untake(untaker);
    }

    /// <summary>
    /// Désactive les IO de la porte pour faciliter la connexion
    /// </summary>
    /// <param name="taker"></param>
    public new void FinishedTaking(GameObject taker)
    {
        taker.GetComponent<Gate>().DisbaleIO();
    }

    /// <summary>
    /// Est-ce qe l'ancre gère les collisions ?
    /// </summary>
    /// <returns>Ce type d'ancre ne les gère pas</returns>
    public new bool HandleCollisions()
    {
        return false;
    }


}
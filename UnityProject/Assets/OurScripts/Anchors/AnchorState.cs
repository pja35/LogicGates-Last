using UnityEngine;

/// <summary>
/// Détaille les informations sur une ancre.
/// </summary>
public class AnchorState : MonoBehaviour, IAnchor
{
   /// <summary>
   /// Si l'ancre est libre ou non
   /// </summary>
    public int free;

    //position de l'ancre en jeu
    private Vector3 position;
    //position de l'ancre dans la matrice d'ancres
    private Vector2 gridPos;

    private GameObject anchor;
    /// <summary>
    /// Initialise l'état d'une ancre
    /// </summary>
    /// <param name="position">La position absolue de l'ancre.</param>
    /// <param name="grid_pos">La position de l'ancre dans la grille.</param>
    public void SetAnchor(Vector3 position, Vector2 grid_pos, int free)
    {
        this.position = position;
        this.gridPos = grid_pos;
        this.free = free;
    }

    /// <summary>
    ///Prend une ancre et place le preneur dessus.
    /// </summary>
    /// <param name="anchor">L'ancre à prendre.</param>
    /// <param name="taker">L'objet prenant l'ancre.</param>
    public void TakeAnchor(GameObject taker)
    {
        this.free = 0;
        taker.transform.position = GetPosition() + new Vector3(0, 0, -10);
        GridCreater grid = GameObject.Find("GridHolder").GetComponent<GridCreater>();
        Gate gate = taker.GetComponent<Gate>();
        if (gate != null)
        {
            grid.SetSurroundingAnchorsState(this, gate, 0);
        }
    }

    /// <summary>
    /// L'objet donné peut il prendre l'ancre
    /// </summary>
    /// <param name="taker">L'objet qui veut prendre l'ancre</param>
    public bool CanTake(GameObject taker)
    {
        Gate gate = taker.GetComponent<Gate>();
        return (free==1 && !GridUtil.IsColliding(this, gate));
    }

    /// <summary>
    /// Libère l'ancre
    /// </summary>
    public void Untake(GameObject untaker)
    {
        free = 1;
        GridCreater grid = GameObject.Find("GridHolder").GetComponent<GridCreater>();
        Gate gate = untaker.GetComponent<Gate>();
        if (gate != null)
        {
            grid.SetSurroundingAnchorsState(this, gate, 1);
        }
    }

    /// <summary>
    /// Informe que l'objet as fini de prendre une ancre (utilisé pour les emplacements)
    /// </summary>
    /// <param name="taker">Le preneur</param>
    public void FinishedTaking(GameObject taker)
    {
        return;
    }

    /// <summary>
    /// Est-ce que cette ancre as un système de colision ?
    /// </summary>
    /// <returns>Ce type d'ancre gère les colisions</returns>
    public bool HandleCollisions()
    {
        return true;
    }

    public Vector3 GetPosition()
    {
        return position;
    }

    public Vector2 GetGridPos()
    {
        return gridPos;
    }

    public bool IsFree()
    {
        return free == 1;
    }

    public bool IsWireFree()
    {
        return free != 0;
    }

    public void SetFreedom(int state)
    {
        free = state;
        
    }
}
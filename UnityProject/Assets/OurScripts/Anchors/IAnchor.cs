using UnityEngine;

/// <summary>
/// Permet de décrire l'état d'une ancre de la grille
/// </summary>
public interface IAnchor
{
    void FinishedTaking(GameObject taker);
    void SetAnchor(Vector3 position, Vector2 grid_pos, int free);
    void SetFreedom(int state);
    void TakeAnchor(GameObject taker);
    void Untake(GameObject untaker);
    bool CanTake(GameObject taker);
    bool HandleCollisions();
    bool IsFree();
    bool IsWireFree();
    Vector2 GetGridPos();
    Vector3 GetPosition();
}
using UnityEngine;

/// <summary>
/// Utilisé pour notifier les objets du jeu d'un changement d'état dans le circuit.
/// </summary>
public abstract class IInOut : MonoBehaviour
{
    public abstract float GetYPlacement();
}

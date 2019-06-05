
/// <summary>
/// Utilisé pour notifier les objets du jeu d'un changement d'état dans le circuit.
/// </summary>
public interface INotifiable {
    /// <summary>
    /// Préviens un objet d'un changement.
    /// </summary>
    void notify();

    
}

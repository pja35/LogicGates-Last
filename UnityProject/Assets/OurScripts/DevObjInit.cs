/// <summary>
/// Permet de gerer les objets placé par les dévellopeurs.
/// </summary>
public interface DevObjInit
{   
    /// <summary>
    /// Place les objets des déveuloppeurs sur la grille.
    /// </summary>
    void PlaceOnGrid();
    /// <summary>
    /// Instantie les objets.
    /// </summary>
    void Instantiate();
    /// <summary>
    /// Crée une connection avec un autre objet au démarrage du jeu.
    /// </summary>
    void MakeInitialConnections();
}

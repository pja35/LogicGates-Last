/// <summary>
/// Permet de gerer les objets placé par les dévellopeurs.
/// </summary>
public interface IDevObjInit
{   
    /// <summary>
    /// Place les objets des développeurs sur la grille.
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

    /// <summary>
    /// Fixe le tag de l'objet
    /// </summary>
    void TagGameObject();
}

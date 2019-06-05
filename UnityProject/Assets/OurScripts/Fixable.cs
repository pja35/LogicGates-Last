
/// <summary>
/// Permet de gerer les objets fixable sur la grille pas l'utilisateur comme les portes.
/// </summary>
public interface IFixable {
    /// <summary>
    /// Quand l'objet est attaché.
    /// </summary>
	void OnFix();
    /// <summary>
    /// Quand l'objet est détaché.
    /// </summary>
	void OnUnfix();
}

/// <summary>
/// Reproduit le comportement d'un porte Non
/// </summary>
public class NOT : Comportement
{
    /// <summary>
    /// Met l'entrée à faux si vraie et inversement
    /// </summary>
    /// <param name="inputs">Liste des entréees de la porte</param>
    /// <returns>Retourne l'inverse de la valeur de la première entrée connectée</returns>
    public override bool CalculateOut(Obj_Input[] inputs)
    {
        if (inputs[0] == null) { return false; }
        return !inputs[0].value;
    }
}


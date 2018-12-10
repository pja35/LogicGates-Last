/// <summary>
/// Reproduit le comportement d'une porte Identité
/// </summary>
public class ID : Comportement
{
    /// <summary>
    /// Renvoie la valeur de la première entrée
    /// </summary>
    /// <param name="inputs">Liste des entrées de la porte</param>
    /// <returns>Retourne vrai si la première entrée est vraie</returns>
    public override bool execute(Obj_Input[] inputs)
    {
        return inputs[0].value;
    }
}


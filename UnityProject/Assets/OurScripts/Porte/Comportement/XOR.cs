
/// <summary>
/// Reproduit le comportement d'un Ou Exclusif
/// </summary>
public class XOR : Comportement
{
    /// <summary>
    /// Renvoie vrai Si les deux entrees ont des valeurs differentes.
    /// </summary>
    /// <param name="inputs">Liste des entréees de la porte</param>
    /// <returns>Retourne vrai si leur nombre est impair</returns>
    public override bool CalculateOut(Obj_Input[] inputs)
    {
        int len = inputs.Length;
        bool result = inputs[0].value;
        for (int i = 1; i < len; i++)
        {
            result = result ^ inputs[i].value;
        }
        return result;
    }
}

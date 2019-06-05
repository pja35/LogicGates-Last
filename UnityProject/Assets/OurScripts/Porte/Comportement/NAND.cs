
/// <summary>
/// Reproduit le comportement d'une porte Non Et
/// </summary>
public class NAND : Comportement
{
    /// <summary>
    /// Verifie que toutes les entrées ne sont pas vraies
    /// </summary>
    /// <param name="inputs">Liste des entréees de la porte</param>
    /// <returns>Retourne vrai si toutes les entrées sont ne sont pas à vrai</returns>
    public override bool CalculateOut(Obj_Input[] inputs)
    {
        int len = inputs.Length;
        bool result = true;
        //Permet de vérifier si la porte est reliée à au moins une autre porte.
        bool existingWiring = false;
        for (int i = 0; i < len; i++)
        {
            if (inputs[i].connection == null) { continue; }
            result = result && inputs[i].value;
            existingWiring = true;
        }
        return (existingWiring) ? !result : false;
    }
}

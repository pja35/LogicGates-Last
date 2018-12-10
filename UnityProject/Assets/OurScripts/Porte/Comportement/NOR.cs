
/// <summary>
/// Reproduit le comportement d'une porte Non Ou
/// </summary>
public class NOR : Comportement
{
    /// <summary>
    /// Verifie que toutes les entrées ne sont pas fausses
    /// </summary>
    /// <param name="inputs">Liste des entréees de la porte</param>
    /// <returns>Retourne vrai si toutes les entrées sont à faux</returns>
    public override bool execute(Obj_Input[] inputs)
    {
        int len = inputs.Length;
        bool result = false;
        //Permet de vérifier si la porte est reliée à au moins une autre porte.
        bool existingWiring = false;
        for (int i = 0; i < len; i++)
        {
            if(inputs[i].connection == null) { continue; }
            result = result || inputs[i].value;
            existingWiring = true;
        }
        return (existingWiring) ? !result : false;
    }
}


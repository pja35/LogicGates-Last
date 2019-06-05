
/// <summary>
/// Reproduit le comportement d'un porte Ou
/// </summary>
public class OR : Comportement{
    /// <summary>
    /// Vérifie qu'il y a au moins une des entrées qui soient vraies.
    /// </summary>
    /// <param name="inputs">Liste des entréees de la porte</param>
    /// <returns>Retourne vrai si une des entrées est vrai</returns>
    public override bool CalculateOut(Obj_Input[] inputs){
		int len = inputs.Length;
		if(len == 1){return inputs[0].value;}
		for(int i=0; i<len; i++){
			if(inputs[i].value){return true;}
		}
		return false;
	}
}

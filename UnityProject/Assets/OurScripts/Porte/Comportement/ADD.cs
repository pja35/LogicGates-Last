/// <summary>
/// Reproduit le comportement d'une porte AND
/// </summary>
public class ADD : Comportement{
    /// <summary>
    /// Verifie que toutes les entrées sont vraies
    /// </summary>
    /// <param name="inputs">Toutes les entrées allant être traitées</param>
    /// <returns>Retourne vrai si toutes les entrées sont à vraies</returns>
    public override bool execute(Obj_Input[] inputs){
		int len = inputs.Length;
		if(len == 1){return inputs[0].value;}
		for(int i=0; i<len; i++){
			if(!inputs[i].value){return false;}
		}
		return true;
	}
}

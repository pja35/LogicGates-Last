/// <summary>
/// Gère le comportement des intérrupteur
/// </summary>
public class Interrupteur : Obj_Input {

    /// <summary>
    /// Un intérrupteur fonctionne comme une sortie mais sa valeur change à chque clic
    /// </summary>
	void OnMouseUp(){
		value = !value;
	}

}

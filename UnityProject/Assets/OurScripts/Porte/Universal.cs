using UnityEngine;

/// <summary>
/// Cette classe permet d'ajouter de nouvelles porte pendant l'execution juste en lui donnant une table de vérité.
/// Cela va permettre de créer et d'enregistrer des modules.
/// Inutilisée pour le moment
/// </summary>
public class Universal : Comportement {

	private bool[] TableVerite;

    /// <summary>
    /// Crée une porte modulaire
    /// </summary>
    /// <param name="tableVerite">le tableau unidimensionnel contenant les sorties de toutes les combinaisons d'entrées.
    /// Exemple: pour une porte AND à deux entrées, le tableau sera[1, 0, 0, 0] correspondant aux entrées en binaire[11, 10, 01, 00]
    /// </param>
    public Universal(bool[] tableVerite){
		this.TableVerite = tableVerite;
	}

    /// <summary>
    /// Cherche dans la table de vérité la sortie en fonction de l'état des entrées.
    /// </summary>
    /// <param name="inputs">Le tableau contenant les liste des entrées</param>
    /// <returns>La nouvelle valeur de sortie</returns>
    public override bool CalculateOut(Obj_Input[] inputs){		
		int a=0;
		for(int i=0; i<inputs.Length; i++){
			int b = (int)(System.Convert.ToInt32(inputs[i].value)*Mathf.Pow(2,i));
			a+=b;
		}
		return TableVerite[a];
	}

}

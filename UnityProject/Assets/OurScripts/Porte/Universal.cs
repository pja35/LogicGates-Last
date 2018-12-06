using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 
	Cette classe permet d'ajouter de nouvelles porte pendant l'execution juste en lui donnant une table de vérité.
	Cela va permettre de créer et d'enregistrer des modules.
**/
public class Universal : Comportement {

	private bool[] tableVerite;

	/**
		\params tableVerite Ce paramètre est le tableau unidimensionnel contenant les sorties de toutes les combinaisons d'entrées.
		Exemple: pour une porte AND à deux entrées, le tableau sera [1,0,0,0] correspondant aux entrées en binaire [11,10,01,00]
	**/
	public Universal(bool[] tableVerite){
		this.tableVerite = tableVerite;
	}
	
	//! Va chercher dans la table de vérité la sortie en fonction des entrées.
	public override bool execute(Obj_Input[] inputs){		
		int a=0;
		for(int i=0; i<inputs.Length; i++){
			int b = (int)(System.Convert.ToInt32(inputs[i].value)*Mathf.Pow(2,i));
			a+=b;
		}
		return tableVerite[a];
	}

}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADD : Comportement{
	//! Verifie que toutes les entrées sont vraies
	public override bool execute(Obj_Input[] inputs){
		int len = inputs.Length;
		if(len == 1){return inputs[0].value;}
		for(int i=0; i<len; i++){
			if(!inputs[i].value){return false;}
		}
		return true;
	}
}

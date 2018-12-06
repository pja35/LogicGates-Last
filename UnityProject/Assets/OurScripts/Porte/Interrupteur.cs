using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interrupteur : Obj_Input {

	void OnMouseUp(){
		value = !value;
	}

}

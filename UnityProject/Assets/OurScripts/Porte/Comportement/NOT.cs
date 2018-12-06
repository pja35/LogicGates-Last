using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOT : Comportement
{
    //! Met l'entrée à faux si vraie et inversement
    public override bool execute(Obj_Input[] inputs)
    {
        if (inputs[0] == null) { return false; }
        return !inputs[0].value;
    }
}


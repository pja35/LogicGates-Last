using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ID : Comportement
{
    //! Valeur de l'entree
    public override bool execute(Obj_Input[] inputs)
    {
        return inputs[0].value;
    }
}


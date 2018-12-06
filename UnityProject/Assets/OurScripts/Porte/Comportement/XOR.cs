using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XOR : Comportement
{
    //! Si les deux entrees ont des valeurs differentes.
    public override bool execute(Obj_Input[] inputs)
    {
        int len = inputs.Length;
        bool result = inputs[0].value;
        for (int i = 1; i < len; i++)
        {
            result = result ^ inputs[i].value;
        }
        return result;
        //if (len == 2)
        //    return inputs[0].value ^ inputs[1].value;
        //else
        //{
        //    return false;
        //}
    }
}

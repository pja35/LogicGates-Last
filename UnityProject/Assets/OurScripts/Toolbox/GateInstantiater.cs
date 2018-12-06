using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateInstantiater : DraggerInstantiater
{
    int nb_inputs, nb_outputs;
    public Comportement exec;
    public string symbol;

    public static GameObject CACGameObject(int nb_inputs, int nb_outputs, Comportement exec, string symbol)
    {
        GameObject inst = GameObject.CreatePrimitive(PrimitiveType.Cube);
        inst.name = "Inst ";

        GateInstantiater gi = inst.AddComponent<GateInstantiater>();
        gi.nb_inputs = nb_inputs;
        gi.nb_outputs = nb_outputs;
        gi.exec = exec;

        DrawUtil.addText(inst, symbol);

        return inst;
    }

    public new void OnMouseDown()
    {
        base.OnMouseDown();
        //GameObject inst, int nb_inputs, int nb_outputs, Comportement comp
		GameObject go = base.cloned;
		Gate.resizeGameObject(go, Mathf.Max(nb_inputs,nb_outputs));
        Gate.createIOGate(go.AddComponent<Gate>(), nb_inputs, nb_outputs, exec);
        cloned.name = "Gate " + symbol;
    }


}

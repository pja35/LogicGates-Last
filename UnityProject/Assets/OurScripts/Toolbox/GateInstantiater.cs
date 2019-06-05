using System;
using UnityEngine;

/// <summary>
/// Un objet qui créera une novelle porte logique en cas de clic
/// Ce script est utilisé pour la toolbox
/// </summary>
public class GateInstantiater : DraggerInstantiater
{

    private int nb_inputs, nb_outputs;
    private Comportement exec;

    /// <summary>
    /// Crée un sprite dans la toolbox qui pourra instancier des portes
    /// </summary>
    /// <param name="nb_inputs">Nombre entrées</param>
    /// <param name="nb_outputs">Nombre sorties</param>
    /// <param name="exec">Le comportement de la future portes</param>
    /// <param name="symbol">Le symbole de la porte.</param>
    /// <returns>Renvoi la porte crée.</returns>
    public static GameObject CreateToolboxGate(int nb_inputs, int nb_outputs, string exec, string symbol)
    {
        GameObject inst = GameObject.CreatePrimitive(PrimitiveType.Cube);
        inst.name = "Inst ";
        GateInstantiater gi = inst.AddComponent<GateInstantiater>();
        gi.nb_inputs = nb_inputs;
        gi.nb_outputs = nb_outputs;

        switch (exec)
        {
            case "&":
                gi.exec = inst.AddComponent<ADD>();
                break;
            case "|":
                gi.exec = inst.AddComponent<OR>();
                break;
            case "!":
                gi.exec = inst.AddComponent<NOT>();
                break;
            case "ID":
                gi.exec = inst.AddComponent<ID>();
                break;
            case "XOR":
                gi.exec = inst.AddComponent<XOR>();
                break;
            case "NAND":
                gi.exec = inst.AddComponent<NAND>();
                break;
            case "!&":
                gi.exec = inst.AddComponent<NAND>();
                break;
            case "NOR":
                gi.exec = inst.AddComponent<NOR>();
                break;
            case "!|":
                gi.exec = inst.AddComponent<NOR>();
                break;
            default:
                Debug.LogError("Initalising toolbox gate with an invalid symbol please refer to toolbox script for available symbols.");
                break;
        }
        DrawUtil.AddText(inst, symbol);

        return inst;
    }

    // Si on clique on instancie une porte que l'on va drag sur la grille.
    private new void OnMouseDown()
    {
        try
        {
           base.OnMouseDown();
                GameObject go = base.cloned;
                Gate gate = go.AddComponent<Gate>();
                gate.CreateGateIO(nb_inputs, nb_outputs, exec);
                gate.notify();
        }
        catch (NullReferenceException)
        {
            Debug.Log("Vous n'avez pas défini de comportement pour: " + gameObject.name);
        }

    }

    /// <summary>
    /// Actualise la couleur du sprite actuel
    /// </summary>
    public void RefreshGatesColor()
    {
        gameObject.GetComponent<Renderer>().material.color = ParametersLoader.GetColor();
    }


}

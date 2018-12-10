using UnityEngine;

public class GateInstantiater : DraggerInstantiater
{
    /// <summary>
    /// Les nombre d'I/O de la future portes
    /// </summary>
    int nb_inputs, nb_outputs;
    /// <summary>
    /// Le comportement de la future portes
    /// </summary>
    public Comportement exec;
    /// <summary>
    /// Le symbole qui sera affiché sur la porte.
    /// </summary>
    public string symbol;

    /// <summary>
    /// Crée un objet pour instancier les portes
    /// </summary>
    /// <param name="nb_inputs">Nombre entrées</param>
    /// <param name="nb_outputs">Nombre sorties</param>
    /// <param name="exec">Le comportement de la future portes</param>
    /// <param name="symbol">Le symbole de la porte.</param>
    /// <returns>Renvoi la porte crée.</returns>
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

    /// <summary>
    /// Si on clique on instancie une porte que l'on va drag sur la grille.
    /// </summary>
    public new void OnMouseDown()
    {
        base.OnMouseDown();
		GameObject go = base.cloned;
		Gate.resizeGameObject(go, Mathf.Max(nb_inputs,nb_outputs));
        //Instanciation de la nouvelle porte.
        Gate.createIOGate(go.AddComponent<Gate>(), nb_inputs, nb_outputs, exec);
        cloned.name = "Gate " + symbol;
    }


}

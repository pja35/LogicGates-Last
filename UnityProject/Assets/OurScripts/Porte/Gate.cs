using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Cette interface permet de mettre en place le patron de conception Stratégie car les portes ont le même comportement exepté aux moment de la verification. 
public abstract class Comportement : MonoBehaviour
{
    public virtual bool execute(Obj_Input[] inputs){return false;}
}

public class Gate : MonoBehaviour, Fixable, Notifiable
{
    /// Toutes les entrées de la porte. 
    public Obj_Input[] inputs;
    /// Toutes les sorties de la porte. 
    public Obj_Output[] outputs;
    /// Cette variable permet de mettre en place le patron stratégie.
    public Comportement exec;

    public bool test = false;

    /// Permet à la porte de lancer une vérification de l'état lorsqu'une de ses entrées utilise cette méthode.
    public void notify()
    {
        Debug.Log("Gate " + gameObject);
        for (int i = 0; i < outputs.Length; i++)
        {
            outputs[i].value = exec.execute(inputs);
            outputs[i].notify();
        }
    }
    /*
    void Update()
    {
        notify();
        if (test)
        {
            Debug.Log("a-");
            Debug.Log(gameObject.name);
            Debug.Log(inputs);
            Debug.Log(outputs);
        }
    }*/

    /// <summary>
    /// Replace la porte et reactive les entrées sorties.
    /// </summary>
    public void OnFix()
    {
        Gate.replaceGameObject(gameObject, Mathf.Max(inputs.Length,outputs.Length));
        for (int i = 0; i < inputs.Length; i++) inputs[i].gameObject.SetActive(true);
        for (int i = 0; i < outputs.Length; i++) outputs[i].gameObject.SetActive(true);
    }
    /// <summary>
    /// Deconnecte toutes les entrées et sorties afin d'éviter que l'on puisse créer des boucles dans les liaisons en deplacant une porte plus bas que
    /// la porte à qui une de ses entrées est connecté.
    /// </summary>
    public void OnUnfix()
    {
        for (int i = 0; i < inputs.Length; i++)
        {
            inputs[i].gameObject.SetActive(false);
            inputs[i].GetComponent<Obj_Input>().Disconnect();
        }
        for (int i = 0; i < outputs.Length; i++)
        {
            outputs[i].gameObject.SetActive(false);
            outputs[i].GetComponent<Obj_Output>().Disconnect();
            outputs[i].GetComponent<Obj_Output>().notify();
        }
    }

    /// <summary>
    /// Cette fonction statique est le moyen de créer et configurer les emplacements et la taille des entrées et des sorties.
    /// </summary>
    /// <param name="gate">Porte à laquel va être attachée les entrées sorties.</param>
    /// <param name="nb_inputs">Nombres entrées disponible sur cette porte.</param>
    /// <param name="nb_outputs">Nombres sorties disponible sur cette porte.</param>
    /// <param name="comp">Permet de determiner le comportement logique qu'aura la porte </param>
    public static void createIOGate(Gate gate, int nb_inputs, int nb_outputs, Comportement comp)
    {
        gate.exec = comp;
        gate.inputs = new Obj_Input[nb_inputs];
		float max = Mathf.Max(nb_inputs,nb_outputs);
		float dstx = GridCreater.dstBetAnch.x;
        // Inutile ?
		float dsty = GridCreater.dstBetAnch.y;
		float dc = ((max-1f)/2f)*dstx;
        //Moyen trouvé pour que les entrées sorties gardent un aspect carré
		float size=(max>1)? 10.1362f - 1.8101f * max + 0.0965f * (max*max):10;
        for (int i = 0; i < nb_inputs; i++)
        {
            gate.inputs[i] = Obj_Input.createInput(gate.gameObject, i);
			Vector3 abc = gate.inputs[i].gameObject.transform.localScale;
			gate.inputs[i].gameObject.transform.localScale = new Vector3(size,(max>1)?5f:10f,abc.z);
			gate.inputs[i].gameObject.transform.localPosition = new Vector3(0, (max > 1) ? - 0.5f : -0.7f, -99f);
			gate.inputs[i].gameObject.transform.position += new Vector3(-dc + dstx*i, 0, 0);
            gate.inputs[i].gameObject.SetActive(false);
        }
        gate.outputs = new Obj_Output[nb_outputs];
        for (int i = 0; i < nb_outputs; i++)
        {
            gate.outputs[i] = Obj_Output.createOutput(gate.gameObject, i);
			Vector3 abc = gate.outputs[i].gameObject.transform.localScale;
			gate.outputs[i].gameObject.transform.localScale = new Vector3(size,(max>1)?5f:12.5f,abc.z);
			gate.outputs[i].gameObject.transform.localPosition = new Vector3(0, (max > 1)? 0.5f : 0.7f, -99f);
			gate.outputs[i].gameObject.transform.position += new Vector3(-dc + dstx*i, 0, 0);
            gate.outputs[i].gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Permet de redimenssionner un gameObject contenant le script Gate en fonction du nombre d'entrées sorties.
    /// Cette méthode doit absolument être appeler avant <see cref="createIOGate()"/> 
    /// car cela modifie les placements relatif des entrées sorties.
    /// </summary>
    /// <param name="g">GameObject allant être affecté par le redimenssionnement</param>
    /// <param name="nb_IO">Doit être le maximum entre le nombre d'entrées et de sorties</param>
	public static void resizeGameObject(GameObject g, float nb_IO){
		if(nb_IO>1){
			Vector3 vec = g.transform.localScale;
			g.transform.localScale = new Vector3(vec.x*nb_IO, vec.y*3f, vec.z);
		}
	}
	
    /// <summary>
    /// Permet de repositionner la porte car son centre est au millieu de l'image et donc non centrée. 
    /// </summary>
    /// <param name="go">GameObject à repositionner</param>
    /// <param name="nb_IO">Maximum entre le nombre d'entrées et de sorties</param>
    public static void replaceGameObject(GameObject go, float nb_IO)
    {
        Vector3 vec2 = go.transform.position;
        float dstx = GridCreater.dstBetAnch.x;
        float dsty = GridCreater.dstBetAnch.y;
        go.transform.position = new Vector3(vec2.x + (dstx * (nb_IO - 1f)) / 2f, vec2.y + dsty / 2, vec2.z);
    }

    public void Destroy()
    {
        foreach (Obj_Input inact in inputs)
        {
            if (inact != null)
            {
                inact.DestroyIn();
            }
        }
        foreach (Obj_Output outact in outputs)
        {
            if (outact != null)
            {
                outact.DestroyOut();
            }
        }
        Destroy(gameObject);
    }
}
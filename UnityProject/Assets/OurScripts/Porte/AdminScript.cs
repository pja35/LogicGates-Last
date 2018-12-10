using UnityEngine;

public class AdminScript : MonoBehaviour, DevObjInit{

	public GameObject[] listToConnecte;
    //La deuxième dimension 
    public int[] connectionsIO; 
	public Comportement comp;
	public int nb_outputs, nb_inputs;
	public string symbol="";
	public AnchorState upperLeftAnchor;
	
    /// <summary>
    /// Configure la porte 
    /// </summary>
    public void Instantiate()
    {
		float nb_IO = Mathf.Max(nb_outputs,nb_inputs);
		Gate.resizeGameObject(gameObject, nb_IO);
        Gate gt = gameObject.AddComponent<Gate>();
        Gate.createIOGate(gt, nb_inputs, nb_outputs, comp);
        gt.OnFix();
				
        DrawUtil.addText(gameObject, symbol);
    }

    public void PlaceOnGrid()
    {
        upperLeftAnchor = GridUtil.TakeNearestAnchor(gameObject);
		//Redimensionnement
		float nb_IO = Mathf.Max(nb_outputs,nb_inputs);
		if(nb_IO<=1){return;}
        //Repositionnement
        Gate.replaceGameObject(gameObject, nb_IO);
    }


    public void MakeInitialConnections()
    {
        Gate gt = gameObject.GetComponent<Gate>();
        for (int i = 0; i < listToConnecte.Length; i++)
        {
            GameObject toCo = listToConnecte[i];
            if (toCo == null) { continue;  }
            int inputToCo = 0;
            if(i < connectionsIO.Length) { inputToCo = connectionsIO[i]; }
            if(Obj_Output.TryToConnect(gt.outputs[i], toCo, inputToCo))
            {
                gt.outputs[i].disconectable = false;
            }
            else { Debug.Log("Failed to connect " + gt.outputs[i] +" to " + toCo); }
        }
    }
}

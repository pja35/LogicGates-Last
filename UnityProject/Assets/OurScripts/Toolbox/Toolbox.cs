using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gère la barre en bas du jeu permetant de placer de nouvelles portes
/// </summary>
public class Toolbox : MonoBehaviour
{
    /// <summary>
    ///ed. Le materiel des portes
    /// </summary>
    public Material material;
    
    /// <summary>
    ///ed. Le type de portes à creer
    /// </summary>
    public List<string> gateType;
    /// <summary>
    ///ed. Le nombre d'entrées de la porte
    /// </summary>
    public List<int> gateNbInputs;
    /// <summary>
    ///ed. Le nombre de sorties de la porte
    /// </summary>
    public List<int> gateNbOutputs;
	
    /// <summary>
    /// Actualise la couleur des portes en jeu et des portes de la toolbox
    /// </summary>
    public  void RefreshGatesColor()
        {
            foreach( Gate gateAct in transform.GetComponentsInChildren<Gate>())
            {
            gateAct.RefreshColor();
            }

            foreach (GateInstantiater gateAct in transform.GetComponentsInChildren<GateInstantiater>())
            {
                gateAct.RefreshGatesColor();
            }
    }
	
    //instancie les portes de la toolbox
    private void Start()
    {
        List<GameObject> gates = new List<GameObject>();
		
        for (int i = 0; i < gateType.Capacity; i++)
        {   
            gates.Add(GateInstantiater.CreateToolboxGate(
                gateNbInputs[i], gateNbOutputs[i], gateType[i], gateType[i]));
        }

        for (int i = 0; i < gates.Count; i++)
        {
            PlaceToolboxDoor(gates, i);
            RescaleToolboxDoor(gates, i);

            gates[i].name += i;
            material.color = ParametersLoader.GetColor();
            gates[i].GetComponent<Renderer>().material = material;
        }
    }

    private void PlaceToolboxDoor(List<GameObject> gates, int gateNumber)
    {
        gates[gateNumber].transform.SetParent(this.transform);
        float Xposition = (gates.Count > 1) ? (gateNumber) * (0.8f / (gates.Count - 1)) - 0.4f : 0;
        Vector3 gatePosition = new Vector3(Xposition, 0, -99);
        gates[gateNumber].transform.localPosition = gatePosition;
    }

    private void RescaleToolboxDoor(List<GameObject> gates, int gateNumber)
    {
        Vector3 gLoc = gates[gateNumber].transform.localScale;
        gates[gateNumber].transform.localScale = new Vector3(gLoc.x * 7.5f, gLoc.y * 7.5f, 1f);
    }
}

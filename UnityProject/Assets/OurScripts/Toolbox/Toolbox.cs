﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class Toolbox : MonoBehaviour
{
    public Material material;
    public List<string> gateType;
    public List<int> gateNbInputs;
    public List<int> gateNbOutputs;
	
	//public List<MiniGate> gatesToAdd;
	
    public void Start()
    {

        List<GameObject> gates = new List<GameObject>();
		
        //Obligé pour pouvoir faire des modification depuis Unity (Violation de OCP).
        for (int i = 0; i < gateType.Capacity; i++)
        {
            Comportement gateComportement;
            switch (gateType[i])
            {
                case "&":
                    gateComportement = new ADD();
                    break;
                case "|":
                    gateComportement = new OR();
                    break;
                case "!":
                    gateComportement = new NOT();
                    break;
                case "ID":
                    gateComportement = new ID();
                    break;
                case "XOR":
                    gateComportement = new XOR();
                    break;
                case "NAND":
                    gateComportement = new NAND();
                    break;
                case "NOR":
                    gateComportement = new NOR();
                    break;
                default:
                    Debug.LogError("Initalising toolbox gate with and invalid symbol please refer to toolbox script for available symbols.");
                    return;
            }
            gates.Add(GateInstantiater.CACGameObject(
                gateNbInputs[i], gateNbOutputs[i], gateComportement, gateType[i]));
        }

        for (int i = 0; i < gates.Count; i++)
        {
            gates[i].transform.SetParent(this.transform);
            gates[i].name += i;
            float X = (gates.Count > 1) ? (i) * (0.8f / (gates.Count - 1)) - 0.4f : 0;
            Vector3 a = new Vector3(X, 0, -99);
            gates[i].transform.localPosition = a;

            Vector3 gLoc = gates[i].transform.localScale;
            gates[i].transform.localScale = new Vector3(gLoc.x * 7.5f, gLoc.y * 7.5f, 1f);
            gates[i].GetComponent<Renderer>().material = material;
        }
    }
}
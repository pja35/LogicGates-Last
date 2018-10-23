using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//!Gestion de l'affichage d'une liaison entre deux portes.
public class Fils : MonoBehaviour
{
	private Door start;
    private Door end;
	//! État du cable 
    public bool powered = false;
    LineRenderer lineRenderer;
    // Use this for initialization
    void Start()
    {
        //var Can = this.GetComponentInParent<Canvas>();
        //start = Can.GetComponentsInChildren<Door>()[1];
        
       
        
    }
	/**
		Permet d'initialiser la porte qui va être traitée ainsi que  les caractéristique du fils de la lisaison.
		\param start est la porte dont on gère l'affichage 
	*/
    public void Init(Door start)
    {
        this.start = start;
        end = start.LinkedTo();
        lineRenderer = this.gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = 0.8f;
        lineRenderer.SetPosition(0, start.gameObject.transform.position);
        lineRenderer.SetPosition(1, end.gameObject.transform.position);
    }

    //! Permet de changer l'état du cable et la position du cable si la porte est modifiée.
    void Update()
    {
        if (powered != start.power)
        {
            powered = start.power;
            end.power = powered;
        }
        lineRenderer.SetPosition(0, start.gameObject.transform.position);
        lineRenderer.SetPosition(1, end.gameObject.transform.position);
       
    }
}

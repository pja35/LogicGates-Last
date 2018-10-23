using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! Définition de base d'une porte logique.
public class Door : MonoBehaviour
{
	// Définie la porte à laquelle elle est reliée
    private Door link;
	// Indique si sa sortie est active
    public bool power = false;
    
	//! Créer un fil entre cette porte et \sa link
    public void Start()
    {
        var Can = this.GetComponentInParent<Canvas>();
        link = Can.GetComponentInChildren<Door>();
        print(this +"  "+ link);
        Fils fil = this.gameObject.AddComponent<Fils>();
        fil.Init(this);
    }
	//! @return Porte à laquelle elle est reliée
    public Door LinkedTo()
    {

        return link;
    }

    // Update is called once per frame
    void Update()
    {
       // print(this + "is linked to" + link);
    }
}

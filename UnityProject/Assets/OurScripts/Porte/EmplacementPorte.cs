using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Une clase pour gerer les emplacements de portes.
/// Un emplacement de porte ne peut recevoir qu'une porte avec le bon nombre d'IO et est doit être utilisé avec une grille désactivée. 
/// Il est possible d'assigner un comportement desiré à cette porte et elle ne fonctionnera qu'avec ce type de porte attendu.
/// </summary>
public class EmplacementPorte : EditorObject, IDevObjInit
{
    /// <summary>
    /// Si non initialisé on autorise tous les types de porte à être inseré.
    /// On donne le nom de la porte située dans la toolbox (Inst 3 par exemple) qui va être autorisée. 
    /// </summary>
    public string answer = "";

    private CageAnchor anchor;

    /// <summary>
    /// Place l'emplacement de la grille et fait aussi le travail de Instantiate.
    /// </summary>
    public new void PlaceOnGrid()
    {
        IAnchor choosen =  GridUtil.TakeNearestAnchor(gameObject);

        if (GameObject.Find("GridHolder").GetComponent<GridCreater>().anchorsCanBeTaken) {
            Debug.LogWarning("Vous essayez d'utiliser un emplacement de porte alors que l'etat initial de la grille n'est pas à faux." +
                " Le comportement normal des emplacement ne pourra pas avoir lieu.");
        }

        //On rajoute une ancre à la grille sur laquelle les portes pouront se fixer. Normalement ces ancres seront les seules disponibles.
        GridCreater grid = GameObject.Find("GridHolder").GetComponent<GridCreater>();
       
        //List<IAnchor> gridAnchors = grid.GetAnchors();
        anchor = gameObject.AddComponent<CageAnchor>();
        anchor.SetAnchor(choosen.GetPosition(), choosen.GetGridPos(), 1,nb_inputs,nb_outputs);

        grid.AdddAnchor(anchor);


    }


    /// <summary>
    /// Recupere le comportement de la porte inseré dans l'emplacement.
    /// Si on n'attend pas un type de porte en particulier on assigne le nouveau comportement dans le cas ou un type de porte est attendu on bloque le comportement non desiré
    /// et on recharge la scene. 
    /// </summary>
    /// <param name="taker"></param>
    public void AnchorTaken(GameObject taker)
    {
        
        Text myText = GetComponentInChildren<Text>();
        string question = taker.GetComponent<Gate>().gateBehaviour.name;
        if (answer == taker.GetComponent<Gate>().gateBehaviour.name || answer.Length == 0)
        {
            myText.text = " ";
            SoundUtil.PlaySound("Sounds/correct");
            Comportement comp = taker.GetComponent<Gate>().gateBehaviour;
            gameObject.GetComponent<Gate>().gateBehaviour = comp;
            gameObject.GetComponent<Gate>().notify();
        }
        else
        {
            SoundUtil.PlaySound("Sounds/wrong");
            SpriteUtil.AddCross(gameObject);
           
            Gate gate = taker.GetComponent<Gate>();
            gate.Destroy();
            Destroy(taker);

            StartCoroutine(waiter());
        }

    }

    /// <summary>
    /// Fonction qui attend 2 seconde avant de recharger la scene
    /// </summary>
    IEnumerator waiter()
    {
        yield return new WaitForSecondsRealtime(2);
        ButtonUtil.StatReloadScene();

    }

    public void AnchorFreed()
    {
        Debug.Log("free");
        gameObject.GetComponent<Gate>().gateBehaviour = null;
        gameObject.GetComponent<Gate>().notify();
    }

}

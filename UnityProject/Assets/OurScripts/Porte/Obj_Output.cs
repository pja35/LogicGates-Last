using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Pour gerer l'objet comme un Output pouvant se connecter à un Input valide.
/// </summary>
public class Obj_Output : MonoBehaviour, Notifiable
{
    //ed. Si le cable peut être détruit ou non. (Utilisé pour les connections initiales des portes dévellopeurs.)
    public bool disconectable;

    //La valeur de l'output.
    public bool value;
    //La connection avec un input si elle existe.
    public Fils connection;

    //Si on est en train de tenter de se connecter.
    private bool connecting = false;
    //Les inputs auquel on peut possiblement se connecter.
    private GameObject[] possiblesConnections = null;
    //L'input auquel se connecter.
    private GameObject nearInput = null;



    public static Obj_Output createOutput(GameObject g, int num)
    {
        return createOutput(g, num, true);
    }

    /// <summary>
    ///   Créer et configure une sortie qui sera attaché au GameObject qui lui est passée.
    /// </summary>
    /// <param name="g">le GameObject auquel va être attaché la sortie.</param>
    /// <param name="num">la num-ième sortie connecté à g.Cela va determiné sa position automatiquement.</param>
    /// <param name="disconectable">Si on peut déconnecter le fil.</param>
    /// <returns>L'output crée.</returns>
    public static Obj_Output createOutput(GameObject g, int num, bool disconectable)
    {
        GameObject inst = (GameObject)Instantiate(Resources.Load("OutputPrefab"), g.transform);

        inst.AddComponent<CircleCollider2D>(); // On ajoute les collisions pour le OnMouseDown

        inst.transform.SetParent(g.transform);
        inst.name = "Output " + num;

        Obj_Output output = (Obj_Output)inst.GetComponent(typeof(Obj_Output));
        output.disconectable = disconectable;
        inst.transform.localPosition = new Vector3(num * 1.5f, +1f, 0f);

        inst.tag = "Output";

        return output;
    }

    /// <summary>
    /// Trouve les objets fils d'un GameObject ayant le tag input.
    /// </summary>
    /// <param name="holder">Le GameObject possedant les inputs.</param>
    /// <returns>La listes des inputs de l'objet.</returns>
    private static GameObject[] FindInputs(GameObject holder)
    {
        List<GameObject> inputs = new List<GameObject>();
        foreach (Transform act in holder.transform)
        {
            if (act.tag == "Input")
            {
                inputs.Add(act.gameObject);
            }
        }
        return inputs.ToArray();
    }


    /// <summary>
    /// Essaie de connecter un input au port inputNumber d'un objet donné. Cette fonction est utile pour la création de niveaux.
    /// </summary>
    /// <param name="output">L'output à connecter.</param>
    /// <param name="toConnect">L'objet auquel se connecter.</param>
    /// <param name="inputNumber">Le numéro de l'objet auquel on veut se connecter.</param>
    public static bool TryToConnect(Obj_Output output, GameObject toConnect, int inputNumber)
    {
        GameObject[] initialConnectionInputList = FindInputs(toConnect);
        if (inputNumber >= initialConnectionInputList.Length)
        {
            LogUtil.LogError(output.gameObject, "Vous essayez sur un numéro d'input supérieur au nombre d'inputs de l'objet auquel vous essayez de vous connecter.\n" +
                "Demandé:" + inputNumber + ", disponibles 0 à: " + (initialConnectionInputList.Length - 1) + ".");
            return false;
        }

        GameObject input = FindInputs(toConnect)[inputNumber];
        if (input.GetComponent<Obj_Input>().connection != null)
        {
            LogUtil.LogError(output.gameObject, "Vous essayez de vous connecter à un input déjà utilisé par un output attaché à "
                + input.GetComponent<Obj_Input>().connection.transform.parent.ToString()
                + "Ceci peut avoir un comportement innatendu en jeu.");
            return false;
        }

        if (input == null)
        {
            LogUtil.LogError(output.gameObject, "On tente de faire une connection initale avec un objet" +
                "n'ayant pas d'input ou avec un mauvais numéro d'input");
            return false;
        }

        output.MakeInitialConnection(
            input);
        output.connection.notify();
        return true;
    }

    public void notify()
    {
        Debug.Log("Output "+gameObject);

        if (connection == null) {return; }
        connection.notify();
    }

    /// <summary>
    /// Supprimer la connection de l'output.
    /// </summary>
    /// <returns>Vrai si une connection as été supprimée.</returns>
    public bool Disconnect()
    {
        if (connection != null && disconectable)
        {
            connection.GetComponent<Fils>().Destroy_Fils();
            return true;
        }
        return false;
    }

    /// <summary>Verifie si la connection est valide</summary>
    /// <returns>true si valide sinon false</returns>
    /// <param name="input">input</param>
    private bool connectionIsValid(GameObject input)
    {
        //Un input est valide si: 
        //On ne se connecte pas à soi même. 
        //L'input est plus haut que soi même. 
        //L'input n'est pas déjà utilisé. 
        //On ne tente pas de se connecter à une lampe en etant un switch.
        return !(
            input.GetComponent<Obj_Input>() == null
            || input.transform.parent == gameObject.transform.parent
            || input.transform.position.y < gameObject.transform.position.y
            || input.GetComponent<Obj_Input>().connection != null
            || (gameObject.transform.parent.tag == "Switch" && input.transform.parent.tag == "Lamp")
            );
    }

    /// <summary>Retourne la connection valide la plus proche de start parmis les input possibles.
    /// <param name="start">start</param>
    /// <param name="possiblesInputs">input possibles</param></summary>
    private GameObject NearestInput(Vector3 start, GameObject[] possiblesInputs)
    {
        GameObject nearest = null;
        float distance = -1;

        foreach (GameObject inputAct in possiblesInputs)
        {
            float newDistance = Vector3.Distance(start, inputAct.transform.position);

            if (connectionIsValid(inputAct) && (newDistance < distance || distance == -1))
            {
                distance = newDistance;
                nearest = inputAct;
            }
        }

        return nearest;
    }

    /// <summary>
    /// Connecte l'output à un input donnée.
    /// </summary>
    /// <param name="input">L'input auquel se connecter.</param>
    public void MakeInitialConnection(GameObject input)
    {
        connection = gameObject.AddComponent<Fils>();
        connection.Init_Fils(input);
        connection.notify();
    }

    /// <summary>
    /// Quand on appui sur la souris on récupère tous les objets de type Input et initialise une ligne.
    /// </summary> 
    private void OnMouseDown()
    {
        //Si on as coupé un fil on ne tente pas imméditement de se connecter.
        if (Disconnect() || !disconectable)
        {
            return;
        }

        connecting = true;
        //Initialise la ligne.
        DrawUtil.DrawLine(gameObject, gameObject, Color.gray);

        possiblesConnections = GameObject.FindGameObjectsWithTag("Input");

    }



    /// <summary>On trace un trait entre la soi-même et la connection valide la plus proche du curseur. Si aucune n'est trouvée on ne trace rien.</summary>
    private void Update()
    {
        if (possiblesConnections == null || !connecting)
        {
            nearInput = null;
            return;

        }

        //On utilise comme point de départ la position du curseur.
        nearInput = NearestInput(Camera.main.ScreenToWorldPoint(Input.mousePosition), possiblesConnections);

        if (nearInput != null)
        {
            DrawUtil.UpdateLine(gameObject, nearInput);
        }
    }

    /// <summary>Au moment de relacher la souris si une connection valide as été trouvée on se connecte à celle-ci.</summary> 
    private void OnMouseUp()
    {
        if (!disconectable)
        {
            return;
        }
        connecting = false;

        if (nearInput != null)
        {
            connection = gameObject.AddComponent<Fils>();
            connection.Init_Fils(nearInput);
        }
    }

    /// <summary>Déconnect l'output et le détruit</summary>
    public void DestroyOut()
    {
        Disconnect();
        Destroy(gameObject);
    }

    /// <summary>
    /// se connecte à un input donnée seulement si la connection est valide.
    /// </summary>
    /// <param name="input">L'input auquel se connecter.</param>
    /// <returns>Vrai si on as pu se connecter sinon faux.</returns>
    /*private bool tryToConnect(GameObject input){
		if(!connectionIsValid(input)){return false;}
		
		connection = gameObject.AddComponent<Fils>();
        connection.Init_Fils(input);
		return true;
	}*/
}

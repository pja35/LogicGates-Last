using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Pour gerer l'objet comme un Output pouvant se connecter à un Input valide.
/// </summary>
public class Obj_Output : IInOut, INotifiable 
{
    /// <summary>
    /// ed. Si le cable peut être détruit ou non. (Utilisé pour les connections initiales des portes dévellopeurs.)
    /// </summary>
    public bool disconectable;
    /// <summary>
    /// La valeur de l'output.
    /// </summary>
    public bool value;
    /// <summary>
    /// La texture quand la sortie est à 1
    /// </summary>
    public Sprite textureOn;
    /// <summary>
    /// La texture quand la sortie est à 0
    /// </summary>
    public Sprite textureOff;

    private Line connection;
    private bool connecting = false;
    private GameObject[] possiblesConnections = null;
    private GameObject nearInput = null;
    private SpriteRenderer image;


    /// <summary>
    /// Crée un output que l'utilisateur pourra déconnecter avec un clic
    /// </summary>
    /// <param name="g">L'objet auquel attacher l'output</param>
    /// <param name="num">Le numéro de l'output</param>
    /// <returns></returns>
    public static Obj_Output CreateDisconactableOutput(GameObject g, int num)
    {
        return CreateOutput(g, num, true);
    }

    /// <summary>
    /// Crée un output que l'utilisateur ne pourra pas déconnecter avec un clic
    /// </summary>
    /// <param name="g">L'objet auquel attacher l'output</param>
    /// <param name="num">Le numéro de l'output</param>
    /// <returns></returns>
    public static Obj_Output CreateUndisconactableOutput(GameObject g, int num)
    {
        return CreateOutput(g, num, false);
    }

    private static Obj_Output CreateOutput(GameObject g, int num, bool disconectable)
    {
        GameObject inst = (GameObject)Instantiate(Resources.Load("OutputPrefab"), g.transform);
        inst.transform.SetParent(g.transform); //Attache les Output à la porte

        inst.AddComponent<CircleCollider2D>(); // On ajoute les collisions pour le 

        Obj_Output output = inst.GetComponent<Obj_Output>();
        output.disconectable = disconectable;

        inst.name = "Output " + num;
        inst.tag = "Output";

        return output;
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
        //Si la connection est valide on se connecte.
        output.MakeInitialConnection(
            input);
        output.connection.notify();
        return true;
    }

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
    /// Indique la position locale que l'output devoir occuper en Y
    /// </summary>
    public override float GetYPlacement()
    {
        return 0.5f;
    }

    /// <summary>
    /// Détruit la connexion si elle est invalide
    /// </summary>
    public void DestroyInvalidConnexion()
    {
        if (connection != null && !connection.IsPLacementValid())
        {
            Disconnect();
        }
    }

    /// <summary>Déconnect l'output et le détruit</summary>
    public void DestroyOut()
    {
        Disconnect();
        Destroy(gameObject);
    }

    /// <summary>
    /// Supprimer la connection de l'output.
    /// </summary>
    /// <returns>Vrai si une connection as été supprimée.</returns>
    public bool Disconnect()
    {
        if (connection != null && disconectable)
        {
            connection.GetComponent<Line>().DestroyLine();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Préviens le fil d'une connection.
    /// </summary>
    public void notify()
    {
        if (connection != null)
        {
            connection.notify();
        }
        ChangeTexture();
    }

    private void ChangeTexture()
    {
        image = GetComponent<SpriteRenderer>();
        image.sprite = (value) ? textureOn : textureOff;
    }

    /// <summary>
    /// Connecte l'output à un input donnée.
    /// </summary>
    /// <param name="input">L'input auquel se connecter.</param>
    public void MakeInitialConnection(GameObject input)
    {
        connection = gameObject.AddComponent<Line>();
        connection.InitLine(input);
        connection.notify();
    }

    /// <summary>
    /// Actualise l'affichage du fil
    /// </summary>
    public void UpdateLineDraw()
    {
        if (connection != null)
        {
            connection.UpdateLineDraw();
        }
    }

    /// <summary>
    /// Actualise l'affichage de la fin du fil
    /// </summary>
    public void UpdateEndOfLine()
    {
        if (connection != null)
        {
            connection.UpdateEndOfLineAndCheckConnection();
        }
    }

    private void OnMouseDown()
    {
        //Si on as coupé un fil on ne tente pas imméditement de se connecter.
        if (Disconnect() || !disconectable)
        {
            return;
        }

        connecting = true;
        //Initalise la ligne
        DrawUtil.DrawLine(gameObject, gameObject);

        possiblesConnections = GameObject.FindGameObjectsWithTag("Input");

    }

    private void OnMouseUp()
    {
        if (disconectable)
        {
            connecting = false;

            if (nearInput != null)
            {
                connection = gameObject.AddComponent<Line>();
                connection.InitLine(nearInput);
            }
        }
    }

    private void Update()
    {
        if (possiblesConnections != null && connecting)
        {
            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            nearInput = NearestValindInput(cursorPosition, possiblesConnections);

            if (nearInput != null)
            { //Trace un fil en utilisant l'algorithme de tracage
                DrawUtil.UpdateLineDraw(gameObject, nearInput);
            }
        } else
        {
            nearInput = null;
        }

        
    }

    private GameObject NearestValindInput(Vector3 start, GameObject[] possiblesInputs)
    {
        GameObject nearest = null;
        float distance = -1;

        foreach (GameObject inputAct in possiblesInputs)
        {
            float newDistance = Vector3.Distance(start, inputAct.transform.position);

            if (ConnectionIsValid(inputAct) && (newDistance < distance || distance == -1))
            {
                distance = newDistance;
                nearest = inputAct;
            }
        }

        return nearest;
    }

    private bool ConnectionIsValid(GameObject input)
    {
        //Un input est valide si: 
        //On ne se connecte pas à soi même. 
        //L'input est plus haut que soi même. 
        //L'input n'est pas déjà utilisé. 
        //On ne tente pas de se connecter à une lampe en etant un switch.
        return (
            input.GetComponent<Obj_Input>() != null
            && input.transform.parent != gameObject.transform.parent
            && input.transform.position.y > gameObject.transform.position.y
            && input.GetComponent<Obj_Input>().connection == null
            && (gameObject.transform.parent.tag != "Switch" || input.transform.parent.tag != "Lamp")
            );
    }

}

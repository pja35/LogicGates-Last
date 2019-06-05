using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Crée une grille d'ancres pour y attacher les portes.
/// </summary>
public class GridCreater : MonoBehaviour
{
    [Range(10, 100)]
    ///<summary>
    ///ed. Le nombre d'ancres par ligne et colonnes.
    ///</summary>
    public int initalAnchorsPerLine = 25;
    /// <summary>
    /// ed. Le matériel des ancres.
    /// </summary>
    public Material anchorsMaterial;
    /// <summary>
    /// ed. Etat inital des ancres. (A utiliser avec des emplacements de portes)
    /// </summary>
    public bool anchorsCanBeTaken = true;
    

	private static Vector3 distBetweenAnchors;
    private int paddingLeft = 2;
    private int paddingRight = 1;
    private int paddingBottom = 3;
    private int paddingTop = 0;

    // Les ancrées qui seront eventuellements rajoutées à la grille
    private List<IAnchor> additionalAnchors = new List<IAnchor>();
    private IAnchor[,] anchorMat;


    /// <summary>
    /// Renvoi la distance séparant chaque ancre de la grille
    /// </summary>
    public static Vector3 GetDistBtwnAnchors()
    {
        return distBetweenAnchors;
    }

    /// <summary>
    /// Génré une grille d'ancres dans un canvas en attachant les ancre au bouton possedant le script.
    /// </summary>
    public void Start()
    {
        SetGridSize();
        anchorMat = new AnchorState[initalAnchorsPerLine - paddingRight - paddingLeft, initalAnchorsPerLine - paddingTop - paddingBottom];
        

        //La position des coins de l'écran.
        Vector3 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 upRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        //De combien décaler l'ancre à chaque étape.
        Vector3 anchorsPerLineAfterPadding = upRight / initalAnchorsPerLine;

        GenerateGridAnchors(bottomLeft, anchorsPerLineAfterPadding);
        distBetweenAnchors = anchorMat[1, 1].GetPosition() - anchorMat[0, 0].GetPosition();

        GameObject[] devPlaced = GameObject.FindGameObjectsWithTag("DevPlaced");
        InstantiateDevPlaced(devPlaced);
        ConnectDevPlaced(devPlaced);

        //NotifyFils();
        
    }

    //utilisé pour adapter la grille selon la plateforme
    private void SetGridSize()
    {
        if (ParametersLoader.getPlatform() == Platform.PC)
        {
            paddingLeft = 2;
            paddingRight = 1;
            paddingBottom = 3;
            paddingTop = 1;
        }
        else
        {
            paddingLeft = 2;
            paddingRight = 1;
            paddingBottom = 3;
            paddingTop = 1;
        }
    }


    private void GenerateGridAnchors(Vector3 bottomLeft, Vector3 steps)
    {
        //Genère les points de la grille.
        for (int x = paddingLeft; x < initalAnchorsPerLine - paddingRight; x++)
        {
            for (int y = paddingBottom; y < initalAnchorsPerLine - paddingTop; y++)
            {
                GameObject anchor = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                Vector3 newAnchorPosition = new Vector3(bottomLeft.x + (steps.x * x * 2), bottomLeft.y + (steps.y * y * 2), 0);
                PlaceAnchor(anchor, newAnchorPosition);
                InitAnchorGameObject(anchor);

                // Crée un objet modèlisant cette ancre.
                IAnchor state = anchor.AddComponent<AnchorState>();
                if(anchorsCanBeTaken){state.SetAnchor(anchor.transform.position, new Vector2(x - paddingLeft, y - paddingBottom), 1);}
                else {state.SetAnchor(anchor.transform.position, new Vector2(x - paddingLeft, y - paddingBottom), 2);}
                anchorMat[x - paddingLeft, y - paddingBottom] = state;
            }
        }
    }

    private void PlaceAnchor(GameObject anchor,Vector3 position)
    {
        //Place la sphère au bon endroit sur l'écran.
        anchor.transform.position = position;
        anchor.transform.SetParent(gameObject.transform);
        Vector3 localPos = anchor.transform.localPosition;
        anchor.transform.localPosition = new Vector3(localPos.x, localPos.y, 0);
    }

    private void InitAnchorGameObject(GameObject anchor)
    {
        //Pour ne pas gerer les colisions sans raison
        Destroy(anchor.GetComponent<SphereCollider>());
        anchor.tag = "Anchor";
        anchor.GetComponent<Renderer>().material = anchorsMaterial;
    }

    private void ConnectDevPlaced(GameObject[] devPlaced)
    {
        foreach (GameObject devObj in devPlaced)
        {
            IDevObjInit initialised = devObj.GetComponent<IDevObjInit>();
            if (initialised != null)
            {
                initialised.MakeInitialConnections();
            }
        }
    }

    private void InstantiateDevPlaced(GameObject[] devPlaced)
    {
        foreach (GameObject devObj in devPlaced)
        {
            IDevObjInit initialised = devObj.GetComponent<IDevObjInit>();
            if (initialised == null)
            {
                Debug.LogError("Trying to initialise a dev placed object not implementing " +
                    "the DevOvjInit interface: " + devObj.ToString());
                continue;
            }
            initialised.Instantiate();
            initialised.PlaceOnGrid();
            initialised.TagGameObject();
        }
    }

   

    /// <summary>
    /// Change l'état des ancres recouvertes par un objet
    /// </summary>
    /// <param name="centerAnchor">L'ancre au centre de l'objet</param>
    /// <param name="gate">La porte permetant d'obtenir la taille d'un objet</param>
    /// <param name="state">L'etat à donner aux ancres</param>
    public void SetSurroundingAnchorsState(IAnchor centerAnchor, Gate gate, int state)
    {
        if (centerAnchor.HandleCollisions())
        {
            Vector2 matPosition = centerAnchor.GetGridPos();
            int sizeX = gate.GetAnchorsX() / 2;
            int sizeY = gate.GetAnchorsY() / 2;
            for (int i = (int)matPosition.x - sizeX; i <= matPosition.x + sizeX; i++)
            {
                for (int j = (int)matPosition.y - sizeY; j <= matPosition.y + sizeY; j++)
                {
                    anchorMat[i, j].SetFreedom(state);
                }
            }
        }
    }

    /// <summary>
    /// Renvoi uniquement les ancres rajoutées à celles de la grille
    /// </summary>
    public List<IAnchor> GetAdditionalAnchors()
    {
        return additionalAnchors;
    }

    /// <summary>
    /// Renvoi toutes les ancres en jeu
    /// </summary>
    public List<IAnchor> GetAllAnchors()
    {
        List < IAnchor > anchors = additionalAnchors;
        foreach( IAnchor act in anchorMat)
        {
            anchors.Add(act);
        }
        return anchors;

    }

    /// <summary>
    /// Renvoi la matrie des ancres (ne contient pas les ancres additionelles)
    /// </summary>
    /// <returns></returns>
    public IAnchor[,] GetMatAnchor()
    {
        return anchorMat;
    }

    /// <summary>
    /// Rajoute une ancre en jeu
    /// </summary>
    public void AdddAnchor(IAnchor anchor)
    {
        additionalAnchors.Add(anchor);
    }

    
}
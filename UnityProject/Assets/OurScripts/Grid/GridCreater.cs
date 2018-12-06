using System.Collections.Generic;
using UnityEngine;





/// <summary>
/// Crée une grille d'ancres pour y attacher les portes.
/// </summary>
public class GridCreater : MonoBehaviour
{
    [Range(10, 100)]
    //ed. Le nombre d'ancres par ligne et colonnes.
    public int grid_divisions = 10;
    //ed. Le matériel des ancres.
    public Material material;
    //ed. Etat inital des ancres. (A utiliser avec des emplacements de portes)
    public bool initialState = true;

    //Position des différent points de référence de la grille
    private int xLeft;
    //Position des différent points de référence de la grille
    private int xRight;
    //Position des différent points de référence de la grille
    private int yBottom;
    //Position des différent points de référence de la grille
    private int yTop;

    // Liste des ancres auquel on pourra accrocher les portes. (Utile pour placer les portes)
    public List<AnchorState> anchor_list = new List<AnchorState>();
    //Sous forme de tableau
    public AnchorState[,] anchor_mat;

    //Distance séparant chaque ancre.
	public static Vector3 dstBetAnch;
	
    /// <summary>
    /// Défini la forme de la grille selon la plateforme sur laquelle tourne le jeu.
    /// Pour le moment inutile.
    /// </summary>
    private void setReferencePoint()
    {
        if (ParametersLoader.getPlatform() == Platform.PC)
        {
            xLeft = 2;
            xRight = 1;
            yBottom = 3;
            yTop = 0;
        }
        else
        {
            xLeft = 2;
            xRight = 1;
            yBottom = 3;
            yTop = 0;
        }
    }

    /// <summary>
    /// Rècupére une ancre dans la grille.
    /// </summary>
    /// <param name="start">L'ancré de départ.</param>
    /// <param name="distx">Le nombre d'ancres de distance en x</param>
    /// <param name="disty">Le nombre d'ancres de distance en y</param>
    /// <returns></returns>
    public AnchorState GetAnchorNear(AnchorState start, int distx, int disty)
    {
        Vector2 designedCase = start.GetGridPos() + new Vector2(distx, disty);
        if (designedCase.x < 0 || designedCase.y < 0 || designedCase.x >= anchor_mat.GetLength(0) || designedCase.y >= anchor_mat.GetLength(1))
        {
            return null;
        }
        return anchor_mat[(int)designedCase.y, (int)designedCase.x];
    }

    /// <summary>
    /// Génré une grille d'ancres dans un canvas en attachant les ancre au bouton possedant le script.
    /// </summary>
    public void Start()
    {
        setReferencePoint();
        anchor_mat = new AnchorState[grid_divisions - xRight - xLeft, grid_divisions - yTop - yBottom];

        //La position des coins de l'écran.
        Vector3 bottom_left = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 up_right = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        //De combien décaler l'ancre à chaque étape.
        Vector3 steps = up_right / grid_divisions;

        //Genère les points de la grille.
        for (int x = xLeft; x < grid_divisions - xRight; x++)
        {
            for (int y = yBottom; y < grid_divisions - yTop; y++)
            {
                GameObject anchor = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                anchor.transform.position = new Vector3(bottom_left.x + (steps.x * x * 2), bottom_left.y + (steps.y * y * 2), 0);
                Destroy(anchor.GetComponent<SphereCollider>());
                anchor.transform.SetParent(gameObject.transform);
                anchor.tag = "Anchor";
                anchor.GetComponent<Renderer>().material = material;
                //Place la sphère au bon endroit sur l'écran.
                anchor.transform.localPosition = new Vector3(anchor.transform.localPosition.x, anchor.transform.localPosition.y, 0);
                
                // Crée un objet modèlisant cette ancre.
                AnchorState state = new AnchorState(anchor.transform.position, new Vector2(x - xLeft, y - yBottom), initialState);
                anchor_mat[x - xLeft, y - yBottom] = state;
                anchor_list.Add(state);
            }
        }

		dstBetAnch = anchor_mat[1,1].GetPosition() - anchor_mat[0,0].GetPosition();
		
        //Récupère tous les objets placés par les dévellopeurs et les initialise.
        GameObject[] devPlaced = GameObject.FindGameObjectsWithTag("DevPlaced");
        foreach (GameObject devObj in devPlaced)
        {
            DevObjInit initialised = devObj.GetComponent<DevObjInit>();
            if (initialised == null)
            {
                Debug.LogError("Trying to initialise a dev placed object not implementing " +
                    "the DevOvjInit interface: " + devObj.ToString());
                continue;
            }
            initialised.Instantiate();
            initialised.PlaceOnGrid();
        }

        //Récupère tous les objets placés par les dévellopeurs et tente de les connecter.
        foreach (GameObject devObj in devPlaced)
        {
            DevObjInit initialised = devObj.GetComponent<DevObjInit>();
            if (initialised == null)
            {
                Debug.LogError("Trying to connect a dev placed object not implementing " +
                    "the DevOvjInit interface: " + devObj.ToString());
                continue;
            }
            initialised.MakeInitialConnections();
        }
        Fils[] filss = GameObject.FindObjectsOfType<Fils>();
        foreach(Fils f in filss)
        {
            f.notify();
        }
    }
}
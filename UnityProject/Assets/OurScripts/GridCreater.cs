using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! C'est une ancre qui va posséder sa position ainsi que sa liberté
public class AnchorState
{
	//! Definie la liberté de l'ancre
    public bool free;
	//! Position de l'ancre
    public Vector3 position;

    public AnchorState(Vector3 position)
    {
        this.position = position;
        free = true;
    }
}

//! Création de la grille de jeu 
public class GridCreater : MonoBehaviour
{
    // Use this for initialization
    [Range(10, 100)]
    public int grid_divisions = 10;
    public Material material;
	
	
	//! Position des différent points de référence de la grille
    public int xLeft;
	//! Position des différent points de référence de la grille
    public int xRight;
	//! Position des différent points de référence de la grille
    public int yBottom;
	//! Position des différent points de référence de la grille
    public int yTop;
	
	
	//! Liste des ancres auquel on pourra accrocher les portes 
    public List<AnchorState> anchor_list = new List<AnchorState>();

	// Placement des points de référence de la grille en fonction du support
	private void setReferencePoint(){
		if(ParametersLoader.getPlatform()==Platform.PC){
			xLeft = 2;
			xRight = -1;
			yBottom = 4;
			yTop = 0;
		}
		else{
			xLeft = 2;
			xRight = -1;
			yBottom = 4;
			yTop = 0;
		}
	}
	
	/** 
	* Cette fonction va permettre définir les points de références et remplir
	* la liste d'ancres qui seront toutes espacées d'une taille définie.
	*/
    public void Start()
    {
		
		setReferencePoint();
		
        Vector3 bottom_left = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 up_right = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        Vector3 step = up_right / grid_divisions;

        for (int x = xLeft; x < grid_divisions - xRight; x++)
        {
            for (int y = yBottom; y < grid_divisions - yTop; y++)
            {
                GameObject anchor = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                anchor.transform.position = new Vector3(bottom_left.x + (step.x * x * 2), bottom_left.y + (step.y * y * 2), 0);
                Destroy(anchor.GetComponent<SphereCollider>());
                anchor.transform.SetParent(gameObject.transform);
                anchor.transform.localPosition = new Vector3(anchor.transform.localPosition.x, anchor.transform.localPosition.y, 0);
                anchor.tag = "Anchor";
                anchor.GetComponent<Renderer>().material = material;
                anchor_list.Add(new AnchorState(anchor.transform.position));
            }
        }
        Debug.Log("Grid size = " + anchor_list.Count);
    }

}

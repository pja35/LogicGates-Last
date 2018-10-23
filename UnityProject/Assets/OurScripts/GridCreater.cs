using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorState
{
    public bool free;
    public Vector3 position;

    public AnchorState(Vector3 position)
    {
        this.position = position;
        free = true;
    }
}

public class GridCreater : MonoBehaviour
{
    // Use this for initialization
    [Range(10, 100)]
    public int grid_divisions = 10;
    public Material material;

    public int xLeft;
    public int xRight;
    public int yBottom;
    public int yTop;

    public List<AnchorState> anchor_list = new List<AnchorState>();

    void Start()
    {
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

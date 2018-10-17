using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    // Use this for initialization
    [Range(10, 100)]
    public int grid_divisions = 10;
    public Material material;
    public List<Vector3> anchor_list = new List<Vector3>();

    void Start()
    {

        Vector3 bottom_left = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 up_right = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        Vector3 step = up_right / grid_divisions;

        for (int x = 2; x < grid_divisions - 2; x++)
        {
            for (int y = 5; y < grid_divisions; y++)
            {
                GameObject anchor = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                anchor.transform.position = new Vector3(bottom_left.x + (step.x * x * 2), bottom_left.y + (step.y * y * 2), 0);
                Destroy(anchor.GetComponent<SphereCollider>());
                anchor.transform.SetParent(gameObject.transform);
                anchor.transform.localPosition = new Vector3(anchor.transform.localPosition.x, anchor.transform.localPosition.y, 0);
                anchor.tag = "Anchor";
                anchor.GetComponent<Renderer>().material = material;
                anchor_list.Add(anchor.transform.position);
            }
        }
    }

}

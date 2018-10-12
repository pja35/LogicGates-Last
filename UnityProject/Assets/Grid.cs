using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    // Use this for initialization
    [Range(10, 100)]
    public int grid_divisions = 10;

    void Start()
    {
        Vector3 bottom_left = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 up_right = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        Vector3 step = up_right / grid_divisions;

        for (int x = 0; x < grid_divisions; x++)
        {
            for (int y = 0; y < grid_divisions; y++)
            {
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.position = new Vector3(bottom_left.x + (step.x * x * 2), bottom_left.y + (step.y * y * 2), 0);
                Destroy(sphere.GetComponent<SphereCollider>());
            }
        }
    }

}

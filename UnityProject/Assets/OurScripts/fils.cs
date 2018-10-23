using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fils : MonoBehaviour
{
    private Door start;
    private Door end;
    public bool powered = false;
    LineRenderer lineRenderer;
    // Use this for initialization
    void Start()
    {
        var Can = this.GetComponentInParent<Canvas>();
        start = Can.GetComponentsInChildren<Door>()[1];
        end = start.LinkedTo();
        lineRenderer = this.gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = 0.8f;
        lineRenderer.SetPosition(0, start.gameObject.transform.position);
        lineRenderer.SetPosition(1, end.gameObject.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (powered != start.power)
        {
            powered = start.power;
            end.power = powered;
        }
        lineRenderer.SetPosition(0, start.gameObject.transform.position);
        lineRenderer.SetPosition(1, end.gameObject.transform.position);

    }
}

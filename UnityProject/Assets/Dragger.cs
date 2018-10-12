using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragger : MonoBehaviour
{

    private Vector3 initialObjMouseDistance;
    private bool mouseDown = false;

    private void OnMouseDown()
    {

        mouseDown = true;
        initialObjMouseDistance = gameObject.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);

    }

    private void OnMouseUp()
    {
        mouseDown = false;
    }

    private void Update()
    {
        if (mouseDown)
        {
            gameObject.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + initialObjMouseDistance;
        }
    }




}

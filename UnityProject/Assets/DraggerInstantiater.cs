using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggerInstantiater : MonoBehaviour
{

    private Vector3 initialObjMouseDistance;
    private bool mouseDown = false;
    private GameObject cloned;

    private void OnMouseDown()
    {
        mouseDown = true;
        initialObjMouseDistance = gameObject.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cloned = Instantiate(gameObject);
        cloned.transform.SetParent(gameObject.transform.parent);
        cloned.transform.localScale = gameObject.transform.localScale;
        Destroy(cloned.GetComponent<DraggerInstantiater>());
        cloned.AddComponent<Dragger>();

    }

    private void OnMouseUp()
    {
        mouseDown = false;
    }

    private void Update()
    {
        if (mouseDown)
        {
            cloned.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + initialObjMouseDistance;
        }
    }




}

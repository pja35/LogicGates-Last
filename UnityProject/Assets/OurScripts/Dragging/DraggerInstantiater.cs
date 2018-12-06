using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Permet de dupliquer un objet et de dragger l'objet cree.
/// </summary>
public class DraggerInstantiater : MonoBehaviour
{
    public GameObject cloned = null;

    /// <summary>
    /// Clone l'objet et rend le clone déplacable.
    /// </summary>
    public void OnMouseDown()
    {
        //Create a new object wich cant't multiply and will follow the cursor
        cloned = Instantiate(gameObject);
        cloned.transform.SetParent(gameObject.transform.parent);
        cloned.transform.localScale = gameObject.transform.localScale;


        cloned.AddComponent<Dragger>();

        //Emulate the OnMouseDown of dragger but with correct values for this specific case
        cloned.GetComponent<Dragger>().mouseDown = true;
        cloned.GetComponent<Dragger>().initialObjMouseDistance = gameObject.transform.position
            - Camera.main.ScreenToWorldPoint(Input.mousePosition);

    }

    /// <summary>
    /// Dépose l'objet et détruit ce script.
    /// </summary>
    public void OnMouseUp()
    {
        cloned.GetComponent<Dragger>().OnMouseUp();
        Destroy(cloned.GetComponent<DraggerInstantiater>());
    }

}

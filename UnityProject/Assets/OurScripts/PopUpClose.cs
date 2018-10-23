using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! Ferme le pop-up auquel il est attribué.
public class PopUpClose : MonoBehaviour
{

	//! Ferme le pop-up.
    public void OnClick()
    {
        //  transform.parent.gameObject.SetActive(false);
        Destroy(transform.parent.gameObject);
    }
}

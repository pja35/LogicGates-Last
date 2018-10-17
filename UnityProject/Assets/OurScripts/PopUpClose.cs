using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpClose : MonoBehaviour
{

    public void OnClick()
    {
        //  transform.parent.gameObject.SetActive(false);
        Destroy(transform.parent.gameObject);
    }
}

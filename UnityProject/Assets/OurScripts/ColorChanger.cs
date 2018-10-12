using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour
{
    public Parameters manager;

    // Use this for initialization
    void Start()
    {
        GetComponent<Image>().color = new Color(manager.color, manager.color, manager.color);
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Image>().color = new Color(manager.color, manager.color, manager.color);
    }
}

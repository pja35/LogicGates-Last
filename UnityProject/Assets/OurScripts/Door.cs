using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Door link;
    public bool power = false;
    // Use this for initialization
    void Start()
    {
        var Can = this.GetComponentInParent<Canvas>();
        link = Can.GetComponentInChildren<Door>();
    }
    public Door LinkedTo()
    {

        return link;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

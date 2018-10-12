using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnoozeChanger : MonoBehaviour
{
    private ParametersLoader manager;

    public void Start()
    {
        manager = (ParametersLoader)GameObject.FindGameObjectsWithTag("Parameters")[0].GetComponent(typeof(ParametersLoader));
    }

    public void OnClick()
    {
        if (manager != null)
        {
            manager.parameters.snooze = !manager.parameters.snooze;
        }
        else
        {
            Debug.Log("Manager not found");
        }
    }
}

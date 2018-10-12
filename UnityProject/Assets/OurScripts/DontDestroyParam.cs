using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyParam : MonoBehaviour
{

    private static bool created = false;

    // Use this for initialization
    void Start()
    {
        if (!created)
        {
            Debug.Log("Speaker Should not be Destroyed");
            created = true;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

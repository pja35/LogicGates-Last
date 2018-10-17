using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroySpeaker : MonoBehaviour
{
    private static bool created = false;

    /*Set the speaker as a don't destroy object*/
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

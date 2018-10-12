using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsSetter : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = 300;
    }
}

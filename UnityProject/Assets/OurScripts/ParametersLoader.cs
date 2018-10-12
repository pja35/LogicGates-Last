using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParametersLoader : MonoBehaviour
{
    public Parameters parameters = new Parameters();

    public void Start()
    {
        parameters.LoadParameters();
    }

}

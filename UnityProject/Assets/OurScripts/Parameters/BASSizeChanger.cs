using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Permet de modifier le nombre d'interrupteur et de lampe dans le BAS.
/// </summary>
public class BASSizeChanger : MonoBehaviour
{

    /// <summary>
    /// Initialise la valeur du slider avec la valeur courante.
    /// </summary>
    public void Start()
    {
        GetComponent<Slider>().value = ParametersLoader.GetBASSize();
    }
    /// <summary>
    /// Si on modifie la valeur du slider on actualise le nombre d'interrupteur et de lampe dans le BAS.
    /// </summary>
    public void OnChange()
    {
        
        ParametersLoader.SetBASSize((int) GetComponent<Slider>().value);
       
    }
}

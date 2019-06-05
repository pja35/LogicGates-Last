using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// La classe ajoute dans la scene un nombre de lampes et d'interrupteurs en fonction
/// des parametres choisit par le joueur dans les options.
/// </summary>
public class GenerateBAS : MonoBehaviour
{
    /// <summary>
    /// L'objet interrupteur à inserer dans la scene
    /// </summary>
    public GameObject prefabSwitch;
    /// <summary>
    /// L'objet lampe à inserer dans la scene
    /// </summary>
    public GameObject prefabLamp;
    /// <summary>
    /// Le nombre d'interrupteurs et de lampes à ajouter dans la scene
    /// </summary>
    private readonly int number = ParametersLoader.GetBASSize();

    /// <summary>
    /// On lance la generation et le placement des objets dans le canvas le plus tôt possible
    /// </summary>
    void Awake()
    {
        addPrefabInCanvas(prefabLamp, number, 650);
        addPrefabInCanvas(prefabSwitch, number, -550);
    }

    /// <summary>
    /// Ajoute dans le canvas un prefab un nombre de fois passé en parametre et à la position verticale en parametre
    /// </summary>
    /// <param name="prefab">Le prefab à placer</param>
    /// <param name="number">Nombre de fois que l'on veut ajouter l'objet</param>
    /// <param name="startYPos">La position verticale à partir de laquelle on va placer les objets</param>
    void addPrefabInCanvas(GameObject prefab, int number, int startYPos)
    {

        for (int i = 0; i < number; i++)
        {

            GameObject newSwitch = Instantiate(prefab, new Vector3(-375 + (i * (800/number) ), startYPos, 0), Quaternion.identity);

            newSwitch.transform.SetParent(GameObject.Find("Canvas").transform, false);

        }

    }

  
}

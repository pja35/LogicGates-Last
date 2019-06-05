using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Parmet d'afficher le niveau actuel
/// </summary>
public class LevelName : MonoBehaviour
{
    /// <summary>
    /// On recupere le nom du niveau pour l'afficher sous la forme de texte dans la scene 
    /// </summary>
    void Start()
    {
        double num;
        num = ButtonUtil.GetLevelNumber();
        GetComponent<Text>().text = GetComponent<Text>().text + num;

    }

}

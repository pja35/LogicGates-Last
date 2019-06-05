using UnityEngine;

/// <summary>Pour éviter la destruction des haut-parleurs. </summary> 
public class DontDestroySpeaker : MonoBehaviour
{
    /// <summary>
    /// Pour savoir si l'objet existe déjà
    /// </summary>
    private static bool created = false;

    /// <summary>Défini l'objet attaché comme un objet non-destructible. </summary> 
    void Start()
    {
        if (!created)
        {
            created = true;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

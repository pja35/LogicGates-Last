using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Utilisé pour détruire l'objet auquel il est attaché si on est hors d'un niveau ou BAS (utilisé pour le bouton "rejouer")
/// </summary>
public class DestroyOnNonLevel : MonoBehaviour
{
    /// <summary>
    /// Verification du niveau actuel et destruction
    /// </summary>
    void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (! (sceneName.Contains("Niveau") || sceneName.Contains("BAS")))
        {
            Destroy(gameObject);
        }
    }

}

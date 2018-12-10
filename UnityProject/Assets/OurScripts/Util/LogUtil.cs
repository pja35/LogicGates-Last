using UnityEngine;

public class LogUtil : MonoBehaviour {

    /// <summary>
    /// Affiche les informations sur l'objet creant le message. 
    /// </summary>
    /// <param name="caster"></param>
    /// <returns>Une chaine formatée avec le nom du lanceur.</returns>
	public static string LogStart(GameObject caster)
    {
        return "Par: " + caster.ToString() + " Fils de:" + caster.transform.parent.ToString();
    }

    /// <summary>
    /// Ecris un message d'erreur.
    /// </summary>
    /// <param name="caster">L'objet lanceur.</param>
    /// <param name="message">Le message</param>
    public static void LogError(GameObject caster, string message)
    {
        Debug.LogError(LogStart(caster) + message);
    }

    /// <summary>
    /// Ecris un message d'avertisement.
    /// </summary>
    /// <param name="caster">L'objet lanceur.</param>
    /// <param name="message">Le message</param>
    public static void LogWarning(GameObject caster, string message)
    {
        Debug.LogWarning(LogStart(caster) + message);
    }
}

using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonUtil : MonoBehaviour
{

    /// <summary>
    /// Increment the unlocked level counteur.
    /// </summary>
    public void UnlockLevel()
    {
        ParametersLoader.UnlockLevel();
    }

    /// <summary>
    /// Decrement the unlocked level counter.
    /// </summary>
    public void LockLevel()
    {
        ParametersLoader.LockLevel();
    }

    /// <summary>
    /// Charge la scène.
    /// </summary>
    /// <param name="sceneToLoad">La scène qui sera chargée..</param>
    public void LoadScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
    }

    /// <summary>
    /// Charge le niveau suivant.
    /// </summary>
    public void LoadNextLevel()
    {
        string newSceneName = ParametersLoader.getNextLevelName(SceneManager.GetActiveScene().name);
        if (Application.CanStreamedLevelBeLoaded(newSceneName))  {
            LoadScene(newSceneName);
        }
        else
        {
            LoadScene("MenuNiveau");
        }
         

    }

    /// <summary>
    /// Charge la scène en mode aditif.
    /// </summary>
    /// <param name="sceneToLoad">La scène qui sera chargée en plus de l'actuelle.</param>
    public void LoadSceneAdd(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);
    }

    /// <summary>
    /// Rechagre la scène courante .
    /// </summary>
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    /// <summary>
    /// Sauvegarde les paramètres.
    /// </summary>
    public void SaveParameters()
    {
        ParametersLoader.SaveParameters();
    }

    /// <summary>
    /// Supprime une scène parmis celles chargées.
    /// </summary>
    /// <param name="sceneToRemove">La scène à supprimer.</param>
    public void RemoveScene(string sceneToRemove)
    {
        SceneManager.UnloadSceneAsync(sceneToRemove);
    }

    /// <summary>
    /// Ferme le jeu.
    /// </summary>
    public void Quit()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                    Application.Quit();
#endif

    }


    /// <summary>
    /// Désactive le panneau actuel et active celui donné.
    /// </summary>
    /// <param name="toActivate">Le panneau à activer.</param>
    public void ChangePanel(GameObject toActivate)
    {
        if (toActivate == null)
        {
            Debug.Log("Trying to switch to a null panel for" + gameObject.ToString());
        }
        gameObject.transform.parent.gameObject.SetActive(false);
        toActivate.SetActive(true);
    }


    /// <summary>
    /// Détruit le parent de l'objet auquel le script est attaché et donc lui même.
    /// </summary>
    public void DestroyParent()
    {
        Destroy(transform.parent.gameObject);
    }

}

using UnityEngine.SceneManagement;
using UnityEngine;
using System;

/// <summary>
/// La liste des fonctions utilisées pour les boutons
/// </summary>
public class ButtonUtil : MonoBehaviour
{

    /// <summary>
    /// Pour connaitre le niveau suivant par rapport au niveau actuel.
    /// </summary>
    /// <returns>Le nom du niveau suivant.</returns>
    public static string GetNextLevelName(string currentSceneName)
    {
        string sceneNumber = currentSceneName.Replace("Niveau", "");
        double num = Convert.ToDouble(sceneNumber);
        string next = "Niveau" + (num + 1);
        return next;
    }

    /// <summary>
    /// Increment the unlocked level counteur.
    /// </summary>
    public void UnlockLevel()
    {
        ParametersLoader.UnlockLevel();
        Debug.Log("niveau actuel : " + ParametersLoader.GetUnlockedLevels());
    }

    /// <summary>
    /// Decrement the unlocked level counter.
    /// </summary>
    public void LockLevel()
    {
        ParametersLoader.LockLevel();
        Debug.Log("niveau actuel : " + ParametersLoader.GetUnlockedLevels());
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
    /// Charge la scène.
    /// </summary>
    /// <param name="sceneToLoad">La scène qui sera chargée..</param>
    static public void StatLoadScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
    }

    /// <summary>
    /// Pour connaitre le niveau précédent par rapport au niveau actuel.
    /// </summary>
    /// <returns>Le nom du niveau suivant.</returns>
    public string GetPreviousLevelName(string currentSceneName)
    {
        //double num = Char.GetNumericValue(currentSceneName, currentSceneName.Length - 1) - 1;
        string sceneNumber = currentSceneName.Replace("Niveau", "");
        double num = Convert.ToDouble(sceneNumber);
        string previous = "Niveau" + (num - 1);
        return previous;
    }

    /// <summary>
    /// Charge le niveau suivant.
    /// </summary>
    public void LoadNextLevel()
    {
        string newSceneName = GetNextLevelName(SceneManager.GetActiveScene().name);

        if (Application.CanStreamedLevelBeLoaded(newSceneName))
        {
            LoadScene(newSceneName);
        }
        else
        {
            LoadScene("Credits");
        }
    }

    /// <summary>
    /// Charge le niveau suivant.
    /// </summary>
    public void LoadPreviousLevel()
    {
        string newSceneName = GetPreviousLevelName(SceneManager.GetActiveScene().name);
        if (Application.CanStreamedLevelBeLoaded(newSceneName))
        {
            LoadScene(newSceneName);
        }
        else
        {
            LoadScene("MenuNiveau");
        }
    }

    /// <summary>
    /// Charge la scène en mode additif.
    /// </summary>
    /// <param name="sceneToLoad">La scène qui sera chargée en plus de l'actuelle.</param>
    public void LoadSceneAdd(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);
    }

    /// <summary>
    /// Recharge la scène courante .
    /// </summary>
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    /// <summary>
    /// Recharge la scène courante .
    /// </summary>
    static public void StatReloadScene()
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
    /// Actualise la couleur des portes en jeu si la scène as une toolbox
    /// </summary>
    public void RefershGatesColor()
    {
        GameObject toolBox = GameObject.Find("Toolbox");
        if (toolBox != null)
        {
            toolBox.GetComponent<Toolbox>().RefreshGatesColor();
        }
        else
        {
            Debug.Log("Les couleurs n'ont pas été actualisées car aucune 'Toolbox' n'existe");
        }
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
    public static void StatQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                            Application.Quit();
#endif
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

    /// <summary>
    /// Ne fait plus apparaitre la popup de tutoriel
    /// </summary>
    public void NoMorePopup()
    {
        //Debug.Log(ParametersLoader.GetTuto());
        ParametersLoader.SetTuto();
        Debug.Log("Les popup tuto sont desormais a " + ParametersLoader.GetTuto());
        SaveParameters();
    }


    /// <summary>
    /// Pour connaitre le numero du niveau actuel.
    /// </summary>
    /// <returns>Le numero du niveau actuel.</returns>
    public static double GetLevelNumber()
    {
        Scene m_Scene = SceneManager.GetActiveScene();
        string sceneName = m_Scene.name;
        string sceneNumber = sceneName.Replace("Niveau", "");
        double num = Convert.ToDouble(sceneNumber);
        return num;
    }

    /// <summary>
    /// Rejoue le theme du menu principal quand on retourne au menu depuis une scene avec un theme different
    /// </summary>
    public void RestartMainTheme()
    {
        SoundUtil.PlayOnlySound("Sounds/theme");
    }

    /// <summary>
    /// Joue le theme des niveaux
    /// </summary>
    public void StartLevelTheme()
    {
        SoundUtil.PlayOnlySound("Sounds/canon");
    }
}

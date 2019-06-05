using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Gère les boutons de changement de niveau en jeu
/// </summary>
public class NextButtonScript : MonoBehaviour
{
    /// <summary>
    /// ed. Le sprite à afficher pour le bouton de changement de niveau
    /// </summary>
    Image image;

    /// <summary>
    /// Change la couleur des flèches de changement de niveau selon si le niveau suivant est débloqué ou non
    /// </summary>
    void Start() {
        image = gameObject.GetComponent<Image>();
        
        int nbLvl = ParametersLoader.GetUnlockedLevels();
        double currentLvl = ButtonUtil.GetLevelNumber();

        if (currentLvl == nbLvl)
        {
            image.color = Color.gray;
        }
    }

    /// <summary>
    /// Récupère le niveau suivant et le charge
    /// </summary>
    public void LoadNextLevel()
    {
        string newSceneName = ButtonUtil.GetNextLevelName(SceneManager.GetActiveScene().name);
        int nbLvl = ParametersLoader.GetUnlockedLevels();
        string numberstring = newSceneName.Replace("Niveau", "");
        double num = Convert.ToDouble(numberstring);
     
        if (num <= nbLvl)
        {
            LoadScene(newSceneName);
        }
    }

    private void LoadScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
    }

}

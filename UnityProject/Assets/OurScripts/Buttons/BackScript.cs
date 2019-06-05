using UnityEngine.SceneManagement;
using UnityEngine;


/// <summary>
/// Gère le comportement du bouton retour sur téléphone
/// </summary>
public class BackScript : MonoBehaviour
{
    void Update() {
        switch (SceneManager.GetActiveScene().name) {
            case "BAS":
                if (Input.GetKeyDown(KeyCode.Escape)) { ButtonUtil.StatLoadScene("MainMenu"); }
                break;
            case "Credits":
                if (Input.GetKeyDown(KeyCode.Escape)) { ButtonUtil.StatLoadScene("MainMenu"); }
                break;
            case "LevelManager":
                if (Input.GetKeyDown(KeyCode.Escape)) { ButtonUtil.StatLoadScene("MainMenu"); }
                break;
            case "MainMenu":
                if (Input.GetKeyDown(KeyCode.Escape)) { ButtonUtil.StatQuit(); }
                break;
            case "MenuNiveau":
                if (Input.GetKeyDown(KeyCode.Escape)) { ButtonUtil.StatLoadScene("MainMenu"); }
                break;
            case "Settings":
                if (Input.GetKeyDown(KeyCode.Escape)) { ButtonUtil.StatLoadScene("MainMenu"); }
                break;
            default:
                if (SceneManager.GetActiveScene().name.Contains("Niveau"))
                    if (Input.GetKeyDown(KeyCode.Escape)) { ButtonUtil.StatLoadScene("MenuNiveau"); }
                break;
        }
    }
}

using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// Gestion de l'affichage des tuto.
/// </summary>
public class TutoChanger : MonoBehaviour
{
    /// <summary>
    /// Initialise la couleur du bouton de tutoriel.
    /// </summary>
    public void Start()
    {
        ChangeColor(ParametersLoader.GetTuto());
    }

    /// <summary>
    /// Inverse l'état du tutoriel.
    /// </summary>
    public void OnClick()
    {
        ParametersLoader.SetTuto();
        Debug.Log("Tuto = " + ParametersLoader.GetTuto());
        ChangeColor(ParametersLoader.GetTuto());
    }

    private void ChangeColor(bool active)
    {
        if (!active)
        {
            gameObject.GetComponent<Image>().color = Color.gray;
        }
        else
        {
            gameObject.GetComponent<Image>().color = Color.white;
        }
    }
}

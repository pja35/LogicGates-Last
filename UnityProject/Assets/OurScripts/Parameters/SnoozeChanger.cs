using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// Gestion du vibreur.
/// </summary>
public class SnoozeChanger : MonoBehaviour
{
    /// <summary>
    /// Initialise la couleur du bouton du vibreur.
    /// </summary>
    public void Start()
    {
        ChangeColor(ParametersLoader.GetSnooze());
    }

    /// <summary>
    /// Inverse l'état du vibreur.
    /// </summary>
    public void OnClick()
    {
        ParametersLoader.SetSnooze(!ParametersLoader.GetSnooze());
        Debug.Log("Snooze = " + ParametersLoader.GetSnooze());
        ChangeColor(ParametersLoader.GetSnooze());
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

using UnityEngine.UI;
using UnityEngine;

//! Gestion du bouton vibreur.
public class SnoozeChanger : MonoBehaviour
{
	//!Initialise la couleur du bouton du vibreur.
    public void Start()
    {
        ChangeColor(ParametersLoader.GetSnooze());
    }

	//! Inverse l'état du vibreur.
    public void OnClick()
    {
        ParametersLoader.SetSnooze(!ParametersLoader.GetSnooze());
        Debug.Log("Snooze = " + ParametersLoader.GetSnooze());
        ChangeColor(ParametersLoader.GetSnooze());
    }

	//! Change la couleur du bouton vibreur pour un retour visuel de son activation.
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

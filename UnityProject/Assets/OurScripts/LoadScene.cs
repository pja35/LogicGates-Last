using UnityEngine;
using UnityEngine.SceneManagement;

/** 
	\brief Classe permettant le chargement d'une autre scène.
	
	Ce script existe car il permet lorsqu'il est attaché à un objet de pouvoir 
	configurer simplement la scène de destination où il ira lorsque l'on cliquera dessus.
*/
public class LoadScene : MonoBehaviour
{
	//! Scène qui sera chargée.
    public string sceneToLoad;

	//! Charge la scène \sa sceneToLoad lorsque l'on clique dessus l'objet.
    public void OnClick()
    {
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
    }
}

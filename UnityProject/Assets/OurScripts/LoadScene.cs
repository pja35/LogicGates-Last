using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadScene : MonoBehaviour
{
    public string sceneToLoad;

    public void OnClick()
    {
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
    }
}

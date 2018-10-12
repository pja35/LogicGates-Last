using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{

    private AssetBundle myLoadedAssetBundle;
    private string[] scenePaths;

    // Use this for initialization
    public void onClick()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
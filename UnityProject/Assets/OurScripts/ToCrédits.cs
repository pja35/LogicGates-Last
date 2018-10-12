using UnityEngine;
using UnityEngine.SceneManagement;

public class ToCrédits : MonoBehaviour {

    private AssetBundle myLoadedAssetBundle;
    private string[] scenePaths;

    public void onClick()
    {
        SceneManager.LoadScene("Credits", LoadSceneMode.Single);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class SettingsLoad : MonoBehaviour {

    private AssetBundle myLoadedAssetBundle;
    private string[] scenePaths;

    // Use this for initialization
    public void onClick () {
        SceneManager.LoadScene("Settings", LoadSceneMode.Single);
    }
}

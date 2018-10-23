using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadSceneAdd : MonoBehaviour {

    public string sceneToLoad;

    public void OnClick()
    {
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);
    }

}

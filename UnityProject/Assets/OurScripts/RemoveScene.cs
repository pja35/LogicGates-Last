using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class RemoveScene : MonoBehaviour {
    public string sceneToRemove;
    public void OnClick()
    {
        SceneManager.UnloadSceneAsync(sceneToRemove);
    }
}

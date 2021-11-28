using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public SceneBlender sceneBlender;
    private bool isLoading;
    private string currentlyLoadedScene;

    public void SwitchScene(string scene)
    {
        if (isLoading)
            return;

        currentlyLoadedScene = scene;
        sceneBlender.StartBlending(() => {LoadScene();});
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(currentlyLoadedScene);
    }
}

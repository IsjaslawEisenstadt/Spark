using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public SceneBlender sceneBlender;

    private bool isLoading;

    public void LoadScene(string scene)
    {
        if (isLoading)
            return;

        sceneBlender.StartBlending(() => LoadSceneNow(scene));
        isLoading = true;
    }

    public void LoadSceneNow(string scene)
    {
        SceneLoader.LoadScene(scene);
    }
}
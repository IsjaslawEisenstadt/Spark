using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public static string currentlyLoadedScene { get; private set; }

    public static void LoadScene(string scene)
    {
        currentlyLoadedScene = scene;
        SceneManager.LoadScene("LoadingScreen");
    }
}

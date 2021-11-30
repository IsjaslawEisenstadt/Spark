using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public SceneBlender sceneBlender;

    public void SwitchScene(string scene) => StartCoroutine(sceneBlender.Fade(FadeMode.FadeOut, () => { SceneManager.LoadScene(scene); }));
}

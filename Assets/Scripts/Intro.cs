using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public SceneLoader sceneLoader;

    void Awake() => GetComponent<UnityEngine.Video.VideoPlayer>().loopPointReached += onEnd;

    void onEnd(UnityEngine.Video.VideoPlayer videoPlayer) => sceneLoader.SwitchScene("MainMenu");

    void Update() 
    {
        if (Input.touchCount > 0)
            onEnd(null);
    }
}

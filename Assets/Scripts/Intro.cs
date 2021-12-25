using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    void Awake() => GetComponent<UnityEngine.Video.VideoPlayer>().loopPointReached += onEnd;

    void onEnd(UnityEngine.Video.VideoPlayer videoPlayer) => SceneManager.LoadScene("MainMenu");
}

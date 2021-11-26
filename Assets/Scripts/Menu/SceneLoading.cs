using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoading : MonoBehaviour
{
    [SerializeField] private Image progressBar;
    [SerializeField] private Animator animator;
    private AsyncOperation scene;

    void Start() => StartCoroutine(LoadAsyncOperator());

    IEnumerator LoadAsyncOperator()
    {
        float lastProgress = 0;
        bool isLoading = false;

        while (lastProgress < 0.9f)
        {
            float waitingTime = 0f;
            if (lastProgress > 0.5f && !isLoading)
            {
                scene = SceneManager.LoadSceneAsync(SceneLoader.currentlyLoadedScene, LoadSceneMode.Additive);
                scene.allowSceneActivation = false;
                isLoading = true;
            }
            else
            {
                waitingTime = Random.RandomRange(0.001f, 0.1f);
                yield return new WaitForSeconds(waitingTime * 10);
            }

            progressBar.fillAmount = !isLoading ? lastProgress + waitingTime : Mathf.Max(scene.progress, lastProgress);
            lastProgress = progressBar.fillAmount;
            yield return new WaitForEndOfFrame();
        }

        progressBar.fillAmount = 1f;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeOutFinished()
    {
        StartCoroutine(SwitchScenes());
    }

    IEnumerator SwitchScenes()
    {
        scene.allowSceneActivation = true;
        yield return scene;
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(SceneLoader.currentlyLoadedScene));
        SceneManager.UnloadSceneAsync("LoadingScreen");
    }
}

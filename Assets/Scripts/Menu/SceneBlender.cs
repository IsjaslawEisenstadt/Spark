using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum FadeMode
{
    FadeIn = 1,
    FadeOut = -1
};

public class SceneBlender : MonoBehaviour
{
    Image image;
    float cutOff = 0;
    float epsilon = 0.01f;

    public float animationSpeed;
    public Material fadeInMaterial;
    public Material fadeOutMaterial;

    void Awake() => image = GetComponent<Image>();

    void Start() => StartCoroutine(Fade(FadeMode.FadeIn));

    public IEnumerator Fade(FadeMode mode, Action callback = null) 
    {
        image.raycastTarget = true;

        image.material = (mode == FadeMode.FadeIn) ? fadeInMaterial : fadeOutMaterial;
        image.material.SetFloat("_CutOff", cutOff);

        do
        {
            cutOff += Mathf.Clamp01(Time.deltaTime * animationSpeed * ((int)mode));
            image.material.SetFloat("_CutOff", cutOff);
            yield return new WaitForEndOfFrame();
        }
        while(cutOff - epsilon >= 0 && cutOff <= 1 + epsilon);

        image.raycastTarget = false;

        if (callback != null)
            callback();
    }
}

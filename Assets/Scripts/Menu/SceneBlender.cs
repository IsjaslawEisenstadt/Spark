using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBlender : MonoBehaviour
{
    public Animator animator;

    private Action callback;

	public void OnFadeOutFinished()
    {
        callback();
    }

    public void StartBlending(Action callback)
    {
        animator.SetTrigger("FadeOut");
        this.callback = callback;
    }
}

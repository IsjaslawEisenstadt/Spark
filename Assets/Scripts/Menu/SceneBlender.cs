using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SceneBlender : MonoBehaviour
{
	Image image;
	Transition currentTransition;

	void Awake() => image = GetComponent<Image>();

	public void StartTransition(TransitionType type, Action onFinishCallback = null, float length = -1.0f,
		EasingFunction.Ease? easingType = null)
	{
		StartCoroutine(Begin(type, onFinishCallback, length, easingType));
	}

	IEnumerator Begin(TransitionType type, Action onFinishCallback, float length, EasingFunction.Ease? easingType)
	{
		currentTransition = gameObject.GetComponent(type.ToString()) as Transition;

		if (!currentTransition)
		{
			Debug.LogError($"SceneBlender doesn't have a Transition of type [{type.ToString()}]");
			yield break;
		}

		image.material = currentTransition.Material;

		currentTransition.EasingType = easingType ?? currentTransition.EasingType;
		if (length >= 0.0f)
		{
			currentTransition.Length = length;
		}

		currentTransition.Restart();

		while (!IsFinished())
		{
			currentTransition.Step(Time.deltaTime);
			image.raycastTarget = currentTransition.IsBlockingClicks();
			yield return new WaitForEndOfFrame();
		}

		image.raycastTarget = false;

		onFinishCallback?.Invoke();
	}

	public bool IsFinished()
	{
		return !currentTransition || currentTransition.IsFinished();
	}
}

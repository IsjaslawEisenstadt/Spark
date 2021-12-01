using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SceneBlender : MonoBehaviour
{
	public TransitionType startTransition = TransitionType.FadeIn;

	Image image;
	Transition currentTransition;

	void Awake() => image = GetComponent<Image>();

	void Start() => StartTransition(startTransition);

	public void StartTransition(TransitionType type, Action onFinishCallback = null, float length = -1.0f,
		EasingFunction.Ease? easingType = null)
	{
		StartCoroutine(Start(type, onFinishCallback, length, easingType));
	}

	IEnumerator Start(TransitionType type, Action onFinishCallback, float length, EasingFunction.Ease? easingType)
	{
		currentTransition = gameObject.GetComponent(type.ToString()) as Transition;
		
		if (!currentTransition)
			yield break;

		image.raycastTarget = true;
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

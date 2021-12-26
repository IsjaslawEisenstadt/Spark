using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SceneBlender : MonoBehaviour
{
	Image image;
	Transition currentTransition;
	bool isReady;

	void Awake() => image = GetComponent<Image>();

	public void StartTransition(TransitionType type, Action onFinishCallback = null, float length = -1.0f,
		EasingFunction.Ease? easingType = null)
	{
		Init(type, length, easingType);
		StartCoroutine(PlayTransition(onFinishCallback));
	}

	public IEnumerator StartAsyncTransition(TransitionType type, Action onFinishCallback = null, float length = -1.0f,
		EasingFunction.Ease? easingType = null)
	{
		Init(type, length, easingType);
		
		isReady = false;
		yield return new WaitUntil(() => isReady);

		StartCoroutine(PlayTransition(onFinishCallback));
	}

	IEnumerator PlayTransition(Action onFinishCallback)
	{
		while (!IsFinished())
		{
			currentTransition.Step(Time.deltaTime);
			image.raycastTarget = currentTransition.IsBlockingClicks();
			yield return new WaitForEndOfFrame();
		}

		image.raycastTarget = false;

		onFinishCallback?.Invoke();
	}

	void Init(TransitionType type, float length = -1.0f, EasingFunction.Ease? easingType = null)
	{
		currentTransition = gameObject.GetComponent(type.ToString()) as Transition;

		if (!currentTransition)
		{
			Debug.LogError($"SceneBlender doesn't have a Transition of type [{type.ToString()}]");
			return;
		}

		image.material = currentTransition.Material;

		currentTransition.EasingType = easingType ?? currentTransition.EasingType;
		if (length >= 0.0f)
		{
			currentTransition.Length = length;
		}

		currentTransition.Restart();
	}

	public bool IsFinished() => !currentTransition || currentTransition.IsFinished();

	public void SetIsReadyForTransition() => isReady = true;
}

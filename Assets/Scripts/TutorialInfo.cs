using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TutorialInfo : MonoBehaviour, IUIElement
{
	public static TutorialInfo Instance { get; private set; }

	public Action<bool> OnVisibilityChanged;

	public TMP_Text title;
	public TMP_Text description;
	public RectTransform button;
	public RectTransform tutorialPopup;
	public RectTransform textContainer;
	public float transformDuration = 1;
	public RectTransform mask;
	[Space(10)] [SerializeField] public List<TutorialStep> tutorialSteps;

	private float buttonHeight;
	private TutorialStep currentTutorialStep;

	void Awake()
	{
		if (Instance != null && Instance != this)
			Destroy(gameObject);
		else
			Instance = this;
	}

	void Start() => buttonHeight = button.sizeDelta.y;

	public void SetupTutorialInfo(TutorialSteps tutorialStep)
	{
		StopAllCoroutines();

		currentTutorialStep = tutorialSteps.FirstOrDefault(x => x.step == tutorialStep);

		OnVisibilityChanged?.Invoke(currentTutorialStep.infoPopupVisible);
		gameObject.SetActive(currentTutorialStep.infoPopupVisible);

		if (!currentTutorialStep.infoPopupVisible)
			return;

		title.text = currentTutorialStep.title;
		description.text = currentTutorialStep.description;

		StartCoroutine(StartPopupTransforms());

		if (currentTutorialStep.mask != null)
		{
			mask.position = currentTutorialStep.mask.position;
			mask.sizeDelta = new Vector2(currentTutorialStep.mask.rect.width, currentTutorialStep.mask.rect.height);
		}
		else
			mask.sizeDelta = Vector2.zero;
	}

	public void OnOpen()
	{
		OnVisibilityChanged(true);
		tutorialPopup.gameObject.SetActive(true);
	}

	public void OnClose()
	{
		OnVisibilityChanged(false);
		tutorialPopup.gameObject.SetActive(false);
	}

	public void OnContinue() => Tutorial.Instance.nextStep(currentTutorialStep.step);

	IEnumerator StartPopupTransforms()
	{
		int currentStepIndex = tutorialSteps.IndexOf(currentTutorialStep);

		if (currentStepIndex == 0 || !tutorialSteps[currentStepIndex - 1].infoPopupVisible)
		{
			SetTargetLayout();
			yield break;
		}

		TutorialStep pastTutorialStep = tutorialSteps[currentStepIndex - 1];

		bool isButtonScaling = !(currentTutorialStep.showContinue == pastTutorialStep.showContinue);
		bool isPopupTranslating = !(currentTutorialStep.popupPosition == pastTutorialStep.popupPosition);
		bool isPopupScaling = !(currentTutorialStep.popupSize == pastTutorialStep.popupSize);

		if (!(isButtonScaling || isPopupTranslating || isPopupScaling))
		{
			SetTargetLayout();
			yield break;
		}

		if (isPopupTranslating)
		{
			Vector3 temp = tutorialPopup.position;
			SetTargetAnchor();
			tutorialPopup.position = temp;
		}

		float buttonStartHeight = button.sizeDelta.y;
		float buttonTargetHeight = currentTutorialStep.showContinue ? buttonHeight : 0;

		Vector2 popupTranslatingStart = tutorialPopup.anchoredPosition;
		Vector2 popupTranslatingTarget = currentTutorialStep.popupPosition.anchoredPosition;

		Vector2 popupScalingStart = textContainer.sizeDelta;
		Vector2 popupScalingTarget = currentTutorialStep.popupSize;

		float timer = 0;

		while (timer < transformDuration)
		{
			timer += Time.deltaTime;
			timer = timer > transformDuration ? transformDuration : timer;

			float percent = timer / transformDuration;

			if (isButtonScaling)
				button.sizeDelta = new Vector2(button.sizeDelta.x,
					Mathf.Lerp(buttonStartHeight, buttonTargetHeight, percent));

			if (isPopupTranslating)
				tutorialPopup.anchoredPosition = Vector3.Lerp(popupTranslatingStart, popupTranslatingTarget, percent);

			if (isPopupScaling)
				textContainer.sizeDelta = Vector3.Lerp(popupScalingStart, popupScalingTarget, percent);

			yield return new WaitForEndOfFrame();
		}
	}

	void SetTargetLayout()
	{
		button.sizeDelta = new Vector2(button.sizeDelta.x, currentTutorialStep.showContinue ? buttonHeight : 0);

		SetTargetAnchor();
		tutorialPopup.anchoredPosition = currentTutorialStep.popupPosition.anchoredPosition;

		textContainer.sizeDelta = currentTutorialStep.popupSize;
	}

	void SetTargetAnchor()
	{
		tutorialPopup.anchorMax = currentTutorialStep.popupPosition.anchorMax;
		tutorialPopup.anchorMin = currentTutorialStep.popupPosition.anchorMin;
	}
}

[Serializable]
public class TutorialStep
{
	public TutorialSteps step;
	public string title;
	[TextArea(5, 15)] public string description;
	public RectTransform popupPosition;
	public Vector2 popupSize;
	public RectTransform mask;
	public bool infoPopupVisible;
	public bool showContinue;
}

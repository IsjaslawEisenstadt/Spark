using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class Tutorial : MonoBehaviour
{
    public static Tutorial Instance { get; private set; }

    public TMP_Text title; 
    public TMP_Text description;
    public Button button;
    public RectTransform tutorialPopup;
    public float transformDuration = 1;
    [SerializeField] public List<TutorialStep> tutorialSteps;
    public string currentStep { get; private set; } 
    private int currentStepIndex;
    

	void Awake()
	{
		if (Instance != null && Instance != this)
			Destroy(gameObject);
		else
            Instance = this;
	}

    void Start()
    {
        currentStepIndex = PlayerPrefs.HasKey("TutorialStep") ? PlayerPrefs.GetInt("TutorialStep") : 0;
        currentStep = tutorialSteps[currentStepIndex].name;
        if (currentStepIndex > 0)
            UIManager.Instance.Open(PopupType.TutorialReset);
        else
            SetupTutorialInfo();
    }

    public void nextStep()
    {
        currentStep = tutorialSteps[++currentStepIndex].name;

        if (currentStepIndex >= tutorialSteps.Count)
            finishTutorial();
        else
            SetupTutorialInfo();
    }

    public void ResetTutorial()
    {
        currentStepIndex = 0;
        PlayerPrefs.SetInt("TutorialStep", currentStepIndex);
        SetupTutorialInfo();
    }

    public void SetupTutorialInfo()
    {
        TutorialStep currentTutorialStep = tutorialSteps[currentStepIndex];
        title.text = currentTutorialStep.name;
        description.text = currentTutorialStep.description;
        tutorialPopup.gameObject.SetActive(currentTutorialStep.infoPopupVisible);
    }

    private void finishTutorial(){}

    private IEnumerator StartPopupTransforms()
    {
        if (currentStepIndex == 0)
        {
            SetTargetLayout();
            yield break;
        }

        bool isButtonScaling = !(tutorialSteps[currentStepIndex].showContinue == tutorialSteps[currentStepIndex - 1].showContinue);
        Vector3 buttonScalingStart = button.transform.localScale;
        Vector3 buttonScalingTarget = new Vector3(1, tutorialSteps[currentStepIndex].showContinue ? 1 : 0, 1);

        bool isPopupTranslating = !(tutorialSteps[currentStepIndex].position == tutorialSteps[currentStepIndex - 1].position);
        Vector3 popupTranslatingStart = tutorialPopup.anchoredPosition;
        Vector3 popupTranslatingTarget = tutorialSteps[currentStepIndex].position;

        if ((!isButtonScaling && !isPopupTranslating)
                || tutorialSteps[currentStepIndex - 1].infoPopupVisible)
        {
            SetTargetLayout();
            yield break;
        }

        float timer = 0;

        while (timer > 0)
        {
            timer += Time.deltaTime;
            timer = timer > transformDuration ? transformDuration : timer;

            if (isButtonScaling)
                button.transform.localScale = Vector3.Lerp(buttonScalingStart, buttonScalingTarget, timer / transformDuration);

            if (isPopupTranslating)
                tutorialPopup.anchoredPosition = Vector3.Lerp(popupTranslatingStart, popupTranslatingTarget, timer / transformDuration);
            
            yield return new WaitForEndOfFrame();
        }
    }

    void SetTargetLayout()
    {
        button.transform.localScale = new Vector3(1, tutorialSteps[currentStepIndex].showContinue ? 1 : 0, 1);
        tutorialPopup.anchoredPosition = tutorialSteps[currentStepIndex].position;
    }
}

[Serializable]
public class TutorialStep
{
    public string name;
    [TextArea(5,15)] public string description;
    public Vector2 position;
    public GameObject mask;
    public GameObject highlight;
    public bool infoPopupVisible;
    public bool showContinue;
    public UnityEvent onContinue;
}

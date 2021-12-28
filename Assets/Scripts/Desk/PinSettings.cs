using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PinSettings : MonoBehaviour, IUIElement
{
	public static PinSettings Instance { get; private set; }

	public GateSelector gateSelector;

	SourceSinkGate currentGate;
	List<Pin> pins;
	int selectedPinIndex;
	string activeScene;

	public TMP_Text pinCountText;
	public TMP_Text selectedPinText;
	public TMP_Text pinActivation;

	[Space(10)] public GameObject pinCount;
	public GameObject divider;
	public GameObject pinSelection;
	public GameObject noSettingsText;

	void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
		}
		else
		{
			Instance = this;
			activeScene = SceneManager.GetActiveScene().name;
		}
	}

	public void OnOpen()
	{
		GameObject hitObject = gateSelector.CurrentSelectedObject.transform.parent.parent.GetComponent<BaseGate>()
			.ActiveGate.gameObject;
		currentGate = hitObject.GetComponent<SourceSinkGate>();

		if (!currentGate)
		{
			OnClose();
			return;
		}

		UIManager.Instance.SetViewport(Viewport.PinSettingOn);
		SetUp(hitObject);
		gameObject.SetActive(true);
	}

	public void OnClose()
	{
		UIManager.Instance.SetViewport(Viewport.PinSettingsOff);
		gameObject.SetActive(false);
	}

	public void OnActivationButton()
	{
		pins[selectedPinIndex].State = !pins[selectedPinIndex].State;
		UpdateSelectedPinView();
	}

	public void OnMinusButton()
	{
		if (pins.Count <= 1)
			return;

		pins.RemoveAt(pins.Count - 1);

		if (selectedPinIndex >= pins.Count)
		{
			selectedPinIndex = pins.Count - 1;
			UpdateSelectedPinView();
		}

		UpdatePinPositions();
		UpdatePinCountView();
	}

	public void OnPlusButton()
	{
		pins.Add(Instantiate(pins[pins.Count - 1],
			currentGate.transform)); //Set pin to the right position is missing

		UpdatePinPositions();
		UpdatePinCountView();
	}

	public void OnLeftButton()
	{
		if (selectedPinIndex > 0)
			selectedPinIndex--;
		else
			selectedPinIndex = pins.Count - 1;

		UpdateSelectedPinView();
	}

	public void OnRightButton()
	{
		if (selectedPinIndex < pins.Count - 1)
			selectedPinIndex++;
		else
			selectedPinIndex = 0;

		UpdateSelectedPinView();
	}

	void SetUp(GameObject activeGate)
	{
		selectedPinIndex = 0;
		pins = currentGate.GateType == GateType.Source ? currentGate.outputs : currentGate.inputs;

		switch (activeScene)
		{
			case ("MissionMode"):
			{
				if (currentGate.GateType == GateType.Source)
				{
					SetUIContext(false, true);
				}
				else //isSink
				{
					SetUIContext(false, false);
				}

				break;
			}
			case ("TutorialMode"):
			{
				//Context of Tutorial is missing
				if (currentGate.GateType == GateType.Source)
				{
					SetUIContext(true, true);
				}
				else //isSink
				{
					SetUIContext(true, false);
				}

				break;
			}
			case ("StudyMode"):
			{
				if (currentGate.GateType == GateType.Source)
				{
					SetUIContext(true, true);
				}
				else //isSink
				{
					SetUIContext(true, false);
				}

				break;
			}
		}

		if (pinCount.activeSelf)
			UpdatePinCountView();
		if (pinSelection.activeSelf)
			UpdateSelectedPinView();
	}

	void UpdateSelectedPinView()
	{
		bool currentPinState = pins[selectedPinIndex].State;
		selectedPinText.text = string.Format("Pin {0}: {1}", selectedPinIndex + 1, currentPinState);
		pinActivation.text = currentPinState ? "deactivate" : "activate";
	}

	void UpdatePinPositions() => currentGate.UpdatePinPositions();

	void UpdatePinCountView() => pinCountText.text = pins.Count.ToString();

	void SetUIContext(bool pinCountActive, bool pinSelectionActive)
	{
		pinCount.SetActive(pinCountActive);
		divider.SetActive(pinCountActive && pinSelectionActive);
		pinSelection.SetActive(pinSelectionActive);
		noSettingsText.SetActive(!(pinCountActive || pinSelectionActive));
	}
}

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PinSettings : MonoBehaviour, IUIElement
{
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

	void Start() => activeScene = SceneManager.GetActiveScene().name;

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
		Init();
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

		Pin pin = pins[pins.Count - 1];
		pin.Clear();
		pins.Remove(pin);
		Destroy(pin.gameObject);

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
		currentGate.CreatePin(currentGate.GateType == GateType.Source ? PinType.Output : PinType.Input);

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

	void Init()
	{
		selectedPinIndex = 0;
		pins = currentGate.GetPins();

		SetUIContext(activeScene != "MissionMode", currentGate.GateType == GateType.Source);

		if (pinCount.activeSelf)
			UpdatePinCountView();
		if (pinSelection.activeSelf)
			UpdateSelectedPinView();
	}

	void UpdateSelectedPinView()
	{
		bool currentPinState = pins[selectedPinIndex].State;
		selectedPinText.text = $"Pin {selectedPinIndex + 1}: {currentPinState}";
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

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PinSettings : MonoBehaviour
{
	public static PinSettings Instance { get; private set; }

	SourceGate currentSource;
	int selectedPinIndex;

	public TMP_Text pinCountText;
	public TMP_Text selectedPinText;
	public TMP_Text pinActivation;

	void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
		}
		else
		{
			Instance = this;
		}
	}

	public void SetVisualizationState(GameObject activeGate)
	{
		if (!activeGate.CompareTag("Source"))
		{
			Close();
			return;
		}

		SetUp(activeGate);
		gameObject.SetActive(true);
	}

	public void Close() => gameObject.SetActive(false);

	public void OnActivationButton()
	{
		currentSource.outputs[selectedPinIndex].State = !currentSource.outputs[selectedPinIndex].State;
		UpdateSelectedPinView();
	}

	public void OnMinusButton()
	{
		if (currentSource.outputs.Count > 1)
		{
			currentSource.outputs.RemoveAt(currentSource.outputs.Count - 1);
			pinCountText.text = currentSource.outputs.Count.ToString();
		}
	}

	public void OnPlusButton()
	{
		currentSource.outputs.Add(Instantiate(currentSource.outputs[currentSource.outputs.Count - 1],
			currentSource.transform)); //Set pin to the right position is missing
		UpdatePinCountView();
	}

	public void OnLeftButton()
	{
		if (selectedPinIndex > 0)
			selectedPinIndex--;
		else
			selectedPinIndex = currentSource.outputs.Count - 1;

		UpdateSelectedPinView();
	}

	public void OnRightButton()
	{
		if (selectedPinIndex < currentSource.outputs.Count - 1)
			selectedPinIndex++;
		else
			selectedPinIndex = 0;

		UpdateSelectedPinView();
	}

	private void SetUp(GameObject activeGate)
	{
		currentSource = activeGate.GetComponent<SourceGate>();
		selectedPinIndex = 0;

		UpdatePinCountView();
		UpdateSelectedPinView();
	}

	private void UpdateSelectedPinView()
	{
		bool currentPinState = currentSource.outputs[selectedPinIndex].State;
		selectedPinText.text = string.Format("Pin {0}: {1}", selectedPinIndex + 1, currentPinState);
		pinActivation.text = currentPinState ? "deactivate" : "activate";
	}

	private void UpdatePinCountView() => pinCountText.text = currentSource.outputs.Count.ToString();
}

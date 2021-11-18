using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSettings : MonoBehaviour
{
    public static PinSettings Instance { get; private set; }

    SourceGate currentSource;
    int selectedPinIndex;

    public Text pinCountText;
    public Text selectedPinText;
    public Button pinActivation;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void CheckAndOpen(GameObject activeGate)
    {
        if (!activeGate.CompareTag("Source"))
            return;

        currentSource = activeGate.GetComponent<SourceGate>();
        setUpUI();
        gameObject.SetActive(true);
    }

    public void Close() => gameObject.SetActive(false);

    public void OnActivationButton() => currentSource.outputs[selectedPinIndex].State = !currentSource.outputs[selectedPinIndex].State;

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
        currentSource.outputs.Add(new Pin()); //the handling of spawning that pin should be done in the constructor of the pin
        pinCountText.text = currentSource.outputs.Count.ToString();
    }

    public void OnLeftButton()
    {
        if (selectedPinIndex > 0)
            selectedPinIndex--;
        else
            selectedPinIndex = currentSource.outputs.Count - 1;
    }

    public void OnRightButton()
    {
        if (selectedPinIndex < currentSource.outputs.Count - 1)
            selectedPinIndex++;
        else
            selectedPinIndex = 0;
    }

    private void setUpUI()
    {
        pinCountText.text = currentSource.outputs.Count.ToString();
        selectedPinText.text = string.Format("Pin {0}: {1}", selectedPinIndex, currentSource.outputs[selectedPinIndex].State);
        pinActivation.GetComponentInChildren<Text>().text = currentSource.outputs[selectedPinIndex].State ? "deactivate" : "activate";
    }
}

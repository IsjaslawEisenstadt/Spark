using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AvailableGates : MonoBehaviour, IUIElement
{
	[SerializeField] GameObject gatePanelPrefab;
	[SerializeField] GameObject HUD;
	Dictionary<String, GameObject> hudPanels = new Dictionary<string, GameObject>();

	void Start()
	{
		MissionState.Instance.availabilityChanged += UpdateGatePlanes;

		if (SceneManager.GetActiveScene().name == "StudyMode")
			return;

		Mission currentMission = CurrentMission.missions[CurrentMission.currentMissionIndex];

		foreach (KeyValuePair<GateType, int> entry in currentMission.GateRestriction)
		{
			AddGatePanel(entry);
		}
			AddGatePanel(new KeyValuePair<GateType, int>(GateType.Sink,1));
			AddGatePanel(new KeyValuePair<GateType, int>(GateType.Source,1));
	}

	void AddGatePanel(KeyValuePair<GateType, int> entry)
	{
		GameObject gatePanelInstance = Instantiate(gatePanelPrefab, HUD.transform);
		gatePanelInstance.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = entry.Key.ToString();
		gatePanelInstance.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = entry.Value.ToString();
		hudPanels.Add(entry.Key.ToString(), gatePanelInstance);
	}

	public void OnOpen(){}

	public void OnClose(){}

	public void UpdateGatePlanes(Dictionary<GateType, GateAvailability> gateAvailabilities)
	{
		foreach (KeyValuePair<GateType, GateAvailability> entry in gateAvailabilities)
		{
			hudPanels[entry.Key.ToString()].transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text =
				(entry.Value.max - entry.Value.currentCount).ToString();
		}
	}
}

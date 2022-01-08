using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AvailableGates : MonoBehaviour, IUIElement
{
	[SerializeField] GameObject gatePanelPrefab;
	[SerializeField] GameObject HUD;
	Dictionary<String, GameObject> hudPanels2 = new Dictionary<string, GameObject>();

	void Start()
	{
		MissionState.Instance.availabilityChanged += UpdateGatePlanes;

		if (SceneManager.GetActiveScene().name != "MissionMode")
			return;

		Mission currentMission = CurrentMission.missions[CurrentMission.currentMissionIndex];

		foreach (KeyValuePair<GateType, int> entry in currentMission.GateRestriction)
		{
			GameObject gatePanelInstance = Instantiate(gatePanelPrefab, HUD.transform);
			GameObject gateName = gatePanelInstance.transform.GetChild(0).gameObject;
			GameObject gateCount = gatePanelInstance.transform.GetChild(1).gameObject;
			gateName.GetComponent<TextMeshProUGUI>().text = entry.Key.ToString();
			gateCount.GetComponent<TextMeshProUGUI>().text = entry.Value.ToString();
			hudPanels2.Add(entry.Key.ToString(),gatePanelInstance);
		}
	}

	public void OnOpen(){}

	public void OnClose(){}

	public void UpdateGatePlanes(Dictionary<GateType, GateAvailability> gateAvailabilities)
	{
		foreach (KeyValuePair<GateType, GateAvailability> entry in gateAvailabilities)
		{
			hudPanels2[entry.Key.ToString()].transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text =
				(entry.Value.max - entry.Value.currentCount).ToString();
		}
	}
}

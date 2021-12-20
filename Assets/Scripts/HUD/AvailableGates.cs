using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AvailableGates : MonoBehaviour
{
	[SerializeField] GameObject gatePanelPrefab;
	void Start()
	{
		Mission currentMission = CurrentMission.missions[CurrentMission.currentMissionIndex];
		GameObject gatePanelInstance;

		foreach (KeyValuePair<GateType, int> entry in currentMission.GateRestriction)
		{
			gatePanelInstance = Instantiate(gatePanelPrefab, transform);
			GameObject gateName = gatePanelInstance.transform.GetChild(0).gameObject;
			GameObject gateCount = gatePanelInstance.transform.GetChild(1).gameObject;
			gateName.GetComponent<TextMeshProUGUI>().text = entry.Key.ToString();
			gateCount.GetComponent<TextMeshProUGUI>().text = entry.Value.ToString();
		}
	}
}

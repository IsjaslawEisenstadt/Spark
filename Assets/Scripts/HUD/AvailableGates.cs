using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AvailableGates : MonoBehaviour, IUIElement
{
	[SerializeField] GameObject gatePanelPrefab;

	void Start()
	{
		if (SceneManager.GetActiveScene().name != "MissionMode")
			return;

		Mission currentMission = CurrentMission.missions[CurrentMission.currentMissionIndex];

		foreach (KeyValuePair<GateType, int> entry in currentMission.GateRestriction)
		{
			GameObject gatePanelInstance = Instantiate(gatePanelPrefab, transform);
			GameObject gateName = gatePanelInstance.transform.GetChild(0).gameObject;
			GameObject gateCount = gatePanelInstance.transform.GetChild(1).gameObject;
			gateName.GetComponent<TextMeshProUGUI>().text = entry.Key.ToString();
			gateCount.GetComponent<TextMeshProUGUI>().text = entry.Value.ToString();
		}
	}

	public void OnOpen(){}

	public void OnClose(){}
}

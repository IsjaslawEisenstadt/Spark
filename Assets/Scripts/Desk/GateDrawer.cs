using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GateDrawer : MonoBehaviour, IUIElement
{
	public GateSelector gateSelector;
	public GameObject gateButtonPrefab;
	public GameObject gateButtonContainer;
	public event Action<BaseGate> onGateTypeChanged;

	BaseGate baseGate;

	void Awake()
	{
		switch (SceneManager.GetActiveScene().name)
		{
			case ("TutorialMode"): break;
			case ("StudyMode"): break;
			case ("MissionMode"): 
			{
				foreach (var entry in CurrentMission.missions[CurrentMission.currentMissionIndex].GateRestriction)
				{
					GameObject gateButton = Instantiate(gateButtonPrefab, gateButtonContainer.transform);
					gateButton.GetComponent<Button>().onClick.AddListener(() => OnGateSelected(entry.Key.ToString() + "Gate"));
					gateButton.transform.GetChild(0).GetComponent<TMP_Text>().text = entry.Key.ToString();
				} 

				break;
			}
		}
	}

	public void OnGateSelected(string gateName)
	{
		baseGate.SetGateType(gateName);
		onGateTypeChanged?.Invoke(baseGate);
	}

	public void OnOpen()
	{
		gameObject.SetActive(true);

		if (baseGate)
			baseGate.Selected = false;

		baseGate = gateSelector.CurrentSelectedObject.transform.parent.parent.gameObject.GetComponent<BaseGate>();
		baseGate.Selected = true;
	}

	public void OnClose()
	{
		if (baseGate)
			baseGate.Selected = false;

		gameObject.SetActive(false);
	}
}

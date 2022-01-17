using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionGateDrawer : GateDrawer
{

	Dictionary<GateType, Button> availableGateButtons  = new Dictionary<GateType, Button>();
	void Start()
	{
		MissionState.Instance.availabilityChanged += SetAvailableGates;
	}

	protected override void GenerateButtons()
	{
		if (CurrentMission.missions == null || CurrentMission.missions.Count <= 0)
		{
			Debug.LogWarning("Generating gate buttons as MissionGateDrawer without available missions...");
			base.GenerateButtons();
		}
		else
		{
			foreach (var entry in CurrentMission.missions[CurrentMission.currentMissionIndex].GateRestriction)
				availableGateButtons[entry.Key] = GenerateGateButton(entry.Key.ToString());

			availableGateButtons[GateType.Sink] = GenerateGateButton("Sink");
			availableGateButtons[GateType.Source] = GenerateGateButton("Source");
		}
	}

	void SetAvailableGates(Dictionary<GateType, GateAvailability> dict)
	{
		foreach (var gateButton in availableGateButtons)
			gateButton.Value.interactable = dict[gateButton.Key].available;
	}
}

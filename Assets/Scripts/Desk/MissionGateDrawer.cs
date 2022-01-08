using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionGateDrawer : GateDrawer
{
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
				GenerateGateButton(entry.Key.ToString());
		}
	}
}

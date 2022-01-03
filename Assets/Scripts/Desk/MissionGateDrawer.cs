using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionGateDrawer : GateDrawer
{
    protected override void GenerateButtons()
	{
		foreach (var entry in CurrentMission.missions[CurrentMission.currentMissionIndex].GateRestriction)
			GenerateGateButton(entry.Key.ToString());
	}
}

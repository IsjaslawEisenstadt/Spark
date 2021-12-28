using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionGateDrawer : GateDrawer
{
    void Awake()
	{
		foreach (var entry in CurrentMission.missions[CurrentMission.currentMissionIndex].GateRestriction)
			GenerateGateButton(entry.Key.ToString());
	}
}

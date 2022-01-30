using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class SourceGate : SourceSinkGate
{
	public override bool[] Evaluate(bool[] values) => outputs.Select(pin => pin.State).ToArray();

	public override void InitGateType() => GateType = GateType.Source;

	public override List<Pin> GetPins() => outputs;

	protected override void Awake()
	{
		if (SceneManager.GetActiveScene().name == "MissionMode")
		{
			int sourcePins = CurrentMission.missions[CurrentMission.currentMissionIndex].sourcePins;

			for (int i = 0; i < sourcePins - 1; ++i)
			{
				CreatePin(PinType.Output);
			}
			UpdatePinPositions();
		}
	}
}

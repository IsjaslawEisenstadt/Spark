using System.Collections.Generic;
using System.Linq;

public class SourceGate : SourceSinkGate
{
	public override bool[] Evaluate(bool[] values) => outputs.Select(pin => pin.State).ToArray();

	public override void InitGateType() => GateType = GateType.Source;

	public override List<Pin> GetPins() => outputs;

	protected override void Awake()
	{
		MissionMode missionMode = MissionMode.Instance;
		if (missionMode && CurrentMission.missions != null && CurrentMission.missions.Count > 0)
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

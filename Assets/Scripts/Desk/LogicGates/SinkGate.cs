using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class SinkGate : SourceSinkGate
{
	public override bool[] Evaluate(bool[] values) => inputs.Select(pin => pin.State).ToArray();

	public override void InitGateType() => GateType = GateType.Sink;

	public override List<Pin> GetPins() => inputs;

	protected override void Awake()
	{
		if (SceneManager.GetActiveScene().name == "MissionMode")
		{
			int sinkPins = CurrentMission.missions[CurrentMission.currentMissionIndex].sinkPins;

			for (int i = 0; i < sinkPins - 1; ++i)
			{
				CreatePin(PinType.Input);
			}
			UpdatePinPositions();
		}
	}
}

using System.Linq;

public class SinkGate : SourceSinkGate
{
	protected override bool[] Evaluate(bool[] values)
	{
		return inputs.Select(pin => pin.State).ToArray();
	}

	public override void InitGateType() => GateType = GateType.Sink;

	public override void UpdatePinPositions()
	{
		//ToDo: Update positions
	}
}

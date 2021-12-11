using System.Linq;

public class SourceGate : SourceSinkGate
{
	protected override bool[] Evaluate(bool[] values)
	{
		return outputs.Select(pin => pin.State).ToArray();
	}

	public override void InitGateType() => GateType = GateType.Source;

	public override void UpdatePinPositions()
	{
		//ToDo: Update positions
	}
}

using System.Collections.Generic;
using System.Linq;

public class SourceGate : SourceSinkGate
{
	public override bool[] Evaluate(bool[] values) => outputs.Select(pin => pin.State).ToArray();

	public override void InitGateType() => GateType = GateType.Source;

	public override List<Pin> GetPins() => outputs;
}

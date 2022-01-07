using System.Collections.Generic;
using System.Linq;

public class SinkGate : SourceSinkGate
{
	public override bool[] Evaluate(bool[] values) => inputs.Select(pin => pin.State).ToArray();

	public override void InitGateType() => GateType = GateType.Sink;

	public override List<Pin> GetPins() => inputs;
}

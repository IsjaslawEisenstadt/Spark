using System;

public class DefaultGate : AbstractGate
{
	public override bool[] Evaluate(bool[] values)
	{
		return Array.Empty<bool>();
	}

	public override void InitGateType() => GateType = GateType.Default;
}

﻿public class OrGate : AbstractGate
{
	protected override bool[] Evaluate(bool[] values)
	{
		return new[] { values[0] | values[1] };
	}

	public override void InitGateTyp() => GateType = GateType.OrGate;
}

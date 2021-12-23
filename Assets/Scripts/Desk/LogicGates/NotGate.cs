public class NotGate : AbstractGate
{
	public override bool[] Evaluate(bool[] values)
	{
		return new[] { !values[0] };
	}

	public override void InitGateType() => GateType = GateType.Not;
}

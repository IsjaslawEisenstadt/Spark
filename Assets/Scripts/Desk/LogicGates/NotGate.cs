public class NotGate : AbstractGate
{
	protected override bool[] Evaluate(bool[] values)
	{
		return new[] { !values[0] };
	}

	public override void InitGateType() => GateType = GateType.Not;
}

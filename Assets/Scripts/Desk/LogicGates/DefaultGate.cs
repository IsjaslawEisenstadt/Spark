public class DefaultGate : AbstractGate
{
	protected override bool[] Evaluate(bool[] values)
	{
		return null;
	}

	public override void InitGateType() => GateType = GateType.Default;
}

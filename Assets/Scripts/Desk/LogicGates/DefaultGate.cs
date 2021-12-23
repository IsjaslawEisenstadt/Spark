public class DefaultGate : AbstractGate
{
	public override bool[] Evaluate(bool[] values)
	{
		return null;
	}

	public override void InitGateType() => GateType = GateType.Default;
}

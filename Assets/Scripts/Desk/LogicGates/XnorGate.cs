public class XnorGate : AbstractGate
{
	protected override bool[] Evaluate(bool[] values)
	{
		return new[] { !(values[0] ^ values[1]) };
	}

	public override void InitGateType() => GateType = GateType.Xnor;
}

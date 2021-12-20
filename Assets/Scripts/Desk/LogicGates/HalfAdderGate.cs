public class HalfAdderGate : AbstractGate
{
    protected override bool[] Evaluate(bool[] values)
    {
	    bool carry = values[0] ^ values[1];
	    bool sum = values[0] & values[1];
	    return new[] { carry, sum };
    }

    public override void InitGateType() => GateType = GateType.HalfAdder;
}

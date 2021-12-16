public class AdderGate : AbstractGate
{
	protected override bool[] Evaluate(bool[] values)
	{
		bool inputOne = values[0];
		bool inputTwo = values[1];
		bool carryBit = values[2];

		bool firstXorGate = inputOne ^ inputTwo;
		bool firstAndGate = inputTwo & inputOne;
		bool secondXorGate = firstXorGate ^ carryBit;
		bool secondAndGate = firstXorGate & carryBit;
		bool orGate = firstAndGate | secondAndGate;
		return new[] { orGate, secondXorGate };
	}

	public override void InitGateType() => GateType = GateType.Adder;
}

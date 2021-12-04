using System.Linq;

public class SinkGate : AbstractGate
{
	protected override bool[] Evaluate(bool[] values)
	{
		return inputs.Select(pin => pin.State).ToArray();
	}
}

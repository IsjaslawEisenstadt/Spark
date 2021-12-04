using System.Linq;

public class SourceGate : AbstractGate
{
	protected override bool[] Evaluate(bool[] values)
	{
		return outputs.Select(pin => pin.State).ToArray();
	}
}

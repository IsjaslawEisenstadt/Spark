using System;

public class DefaultGate : AbstractGate
{
	protected virtual void Awake() => base.Awake();

	public override bool[] Evaluate(bool[] values)
	{
		return Array.Empty<bool>();
	}

	public override void InitGateType() => GateType = GateType.Default;
}

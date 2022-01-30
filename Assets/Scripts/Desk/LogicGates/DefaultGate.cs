using System;
using UnityEngine.SceneManagement;

public class DefaultGate : AbstractGate
{
	protected override void Awake()
	{
		if (SceneManager.GetActiveScene().name == "TutorialMode")
			Tutorial.Instance.nextStep(TutorialSteps.MARKER_ADVICE);

		base.Awake();
	}

	public override bool[] Evaluate(bool[] values)
	{
		return Array.Empty<bool>();
	}

	public override void InitGateType() => GateType = GateType.Default;
}

public class DefaultGateTutorial : DefaultGate
{
	protected override void Awake()
	{
		Tutorial.Instance.nextStep(TutorialSteps.SELECT_GATE);
		base.Awake();
	}
}

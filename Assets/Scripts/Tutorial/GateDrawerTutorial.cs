
public class GateDrawerTutorial : MissionGateDrawer
{
	 public override void OnOpen()
	 {
		Tutorial.Instance.nextStep(TutorialSteps.SELECT_GATE);
		base.OnOpen();
	 }
}

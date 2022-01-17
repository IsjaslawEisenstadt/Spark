public class MissionInfoTutorial : MissionInfo
{
	public override void OnClose()
	{
		Tutorial.Instance.nextStep(TutorialSteps.TAP_START_ON_MISSIONINFO);
		base.OnClose();
	}
}

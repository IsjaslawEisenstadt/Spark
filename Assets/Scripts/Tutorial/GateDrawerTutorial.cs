
public class GateDrawerTutorial : MissionInfoTutorial
{
	void Awake()
	{
		Tutorial.Instance.nextStep(TutorialSteps.SELECT_GATE_SHOW_GATE_DRAWER);
		base.OnClose();
	}
}

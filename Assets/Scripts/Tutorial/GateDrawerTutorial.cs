using UnityEngine;
using UnityEngine.UI;

public class GateDrawerTutorial : MissionGateDrawer
{
	public override void OnOpen()
	{
		Tutorial.Instance.nextStep(TutorialSteps.SELECT_GATE);
		base.OnOpen();
	}

	protected override Button GenerateGateButton(string gateName)
	{
		Button button = base.GenerateGateButton(gateName);

		if (gateName == GateType.And.ToString())
		{
			TutorialInfo.Instance.tutorialSteps[(int)TutorialSteps.LOGIC_GATE_SELECTED].mask =
				button.transform as RectTransform;
			button.onClick.AddListener(() => Tutorial.Instance.nextStep(TutorialSteps.LOGIC_GATE_SELECTED));
		}

		return button;
	}
}

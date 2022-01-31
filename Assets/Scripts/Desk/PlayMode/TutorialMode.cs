public class TutorialMode : PlayMode
{
	ResultView resultView;

	protected override void EvaluatePlay(TruthTableRow[] result)
	{
		UIManager.Instance.Open(PopupType.ResultView);

		if (!resultView)
			resultView = UIManager.Instance.GetPopup(PopupType.ResultView).popup.GetComponent<ResultView>();

		resultView.UpdateNextButtonState(true);

		Tutorial.Instance.nextStep(TutorialSteps.RESULT_IS_VALID);
	}
}

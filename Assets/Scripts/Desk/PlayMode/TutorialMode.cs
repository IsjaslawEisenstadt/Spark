public class TutorialMode : PlayMode
{
	ResultView resultView;
	bool isValid = true;

	protected override void EvaluatePlay(TruthTableRow[] result)
	{
		UIManager.Instance.Open(PopupType.ResultView);

		if (!resultView)
			resultView = UIManager.Instance.GetPopup(PopupType.ResultView).popup.GetComponent<ResultView>();

		resultView.UpdateNextButtonState(isValid);

		if (isValid)
			Tutorial.Instance.nextStep(TutorialSteps.RESULT_IS_VALID);
	}
}

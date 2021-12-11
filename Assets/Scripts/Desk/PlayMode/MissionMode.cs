using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionMode : PlayMode
{
	ResultView resultView;
	bool isValid = true;

	protected override void EvaluatePlay(TruthTableRow[] result)
	{
		UIManager.Instance.Open(PopupType.ResultView);

		if (!resultView)
			resultView = UIManager.Instance.GetElement(PopupType.ResultView).GetComponent<ResultView>();

		resultView?.UpdateNextButtonState(isValid);
		OnResultIsValid();
	}

	private void OnResultIsValid()
	{
		PlayerPrefs.SetInt(CurrentMission.missions[CurrentMission.currentMissionIndex].name, 1);
	}
}

public static class CurrentMission
{
	public static int currentMissionIndex;
	public static List<Mission> missions;
}

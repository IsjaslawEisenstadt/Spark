using System;
using System.Collections.Generic;
using UnityEngine;

public class MissionMode : PlayMode
{
	public static MissionMode Instance { get; private set; }

	ResultView resultView;
	bool isValid = true;

	void Awake() => Instance = this;

	protected override void EvaluatePlay(TruthTableRow[] result)
	{
		UIManager.Instance.Open(PopupType.ResultView);

		if (!resultView)
			resultView = UIManager.Instance.GetPopup(PopupType.ResultView).popup.GetComponent<ResultView>();

		resultView.UpdateNextButtonState(isValid);

		OnResultIsValid();
	}

	void OnResultIsValid()
	{
		SaveMissionCompleted();
		UIManager.Instance.Open(PopupType.RewardWindow);
	}

	void SaveMissionCompleted()
	{
		PlayerPrefs.SetInt("NextMission", CurrentMission.currentMissionIndex + 1);

		string newGate = CurrentMission.missions[CurrentMission.currentMissionIndex].gateScript.GateType.ToString();

		if (!PlayerPrefs.HasKey("AvailableGates"))
			PlayerPrefs.SetString("AvailableGates", newGate);
		else
		{
			string ppString = PlayerPrefs.GetString("AvailableGates");
			List<string> availableGates = new List<string>(ppString.Split(','));
			if (!availableGates.Contains(newGate))
				PlayerPrefs.SetString("AvailableGates", ppString + ',' + newGate);
		}

		PlayerPrefs.Save();
	}
}

public static class CurrentMission
{
	public static int currentMissionIndex;
	public static List<Mission> missions;
}

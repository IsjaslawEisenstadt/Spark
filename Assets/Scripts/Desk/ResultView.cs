using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultView : MonoBehaviour
{
	public SceneLoader sceneLoader;
	public Button nextButton;

	public void OnNextMission()
	{
		CurrentMission.currentMissionIndex++;
		sceneLoader.SwitchScene("MissionMode");
	}

	public void UpdateNextButtonState(bool valid)
	{
		nextButton.interactable = valid;
		nextButton.gameObject.SetActive(CurrentMission.currentMissionIndex < CurrentMission.missions.Count - 1);
	}
}

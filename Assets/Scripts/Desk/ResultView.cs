using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultView : MonoBehaviour, IUIElement
{
	public SceneLoader sceneLoader;
	public Button nextButton;

	public void OnClose()
	{
		throw new System.NotImplementedException();
	}

	public void OnNextMission()
	{
		CurrentMission.currentMissionIndex++;
		sceneLoader.SwitchScene("MissionMode");
	}

	public void OnOpen()
	{
		throw new System.NotImplementedException();
	}

	public void UpdateNextButtonState(bool valid)
	{
		nextButton.interactable = valid;
		nextButton.gameObject.SetActive(CurrentMission.currentMissionIndex < CurrentMission.missions.Count - 1);
	}
}

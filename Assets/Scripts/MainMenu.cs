using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
	public SceneLoader sceneLoader;
	public Mission mission;

	public void OnTutorial()
	{
		CurrentMission.missions = new List<Mission>();
		CurrentMission.missions.Add(mission);
		CurrentMission.currentMissionIndex = 0;
		sceneLoader.SwitchScene("TutorialMode");
	}
}

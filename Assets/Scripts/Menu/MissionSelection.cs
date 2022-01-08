using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MissionSelection : MonoBehaviour
{
	public SceneLoader sceneLoader;
	public GameObject mission;
	public List<Mission> missions;
	public string nextScene = "MissionMode";

	void Start()
	{
		GameObject missionInstance;
		CurrentMission.missions = missions;
		for (int i = 0; i < missions.Count; i++)
		{
			missionInstance = Instantiate(mission, transform);
			missionInstance.transform.GetChild(0).GetComponent<TMP_Text>().text = (i + 1).ToString();
			missionInstance.transform.GetChild(2).GetComponent<TMP_Text>().text = missions[i].name;
			int index = i;
			Button button = missionInstance.GetComponent<Button>();
			button.onClick.AddListener(delegate { OnSelectMission(index); });

			if (!PlayerPrefs.HasKey("NextMission"))
				PlayerPrefs.SetInt("NextMission", 0);

			button.interactable = PlayerPrefs.GetInt("NextMission") >= i;
		}
		PlayerPrefs.Save();
	}

	public void OnSelectMission(int index)
	{
		CurrentMission.currentMissionIndex = index;
		sceneLoader.SwitchScene(nextScene);
	}
}

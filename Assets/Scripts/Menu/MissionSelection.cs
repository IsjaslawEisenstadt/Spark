using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MissionSelection : MonoBehaviour
{
	public SceneLoader sceneLoader;
	public GameObject mission;
	public List<Mission> missions;

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

			if (!PlayerPrefs.HasKey(missions[i].name))
				PlayerPrefs.SetInt(missions[i].name, 0);

			button.interactable = i == 0 || PlayerPrefs.GetInt(missions[i - 1].name) != 0;
			PlayerPrefs.Save();
		}
	}

	public void OnSelectMission(int index)
	{
		CurrentMission.currentMissionIndex = index;
		sceneLoader.SwitchScene("MissionMode");
	}
}

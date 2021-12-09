using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MissionSelection : MonoBehaviour
{
	public SceneLoader sceneLoader;
	public GameObject entry;
	public List<Mission> missions;

	void Start()
	{
		GameObject entryInstance;
		for (int i = 0; i < missions.Count; i++)
		{
			entryInstance = Instantiate(entry, transform);
			entryInstance.transform.GetChild(0).GetComponent<TMP_Text>().text = (i + 1).ToString();
			entryInstance.transform.GetChild(2).GetComponent<TMP_Text>().text = missions[i].name;
			int index = i;
			entryInstance.GetComponent<Button>().onClick.AddListener(delegate { OnSelectMission(index); });
		}
	}

	public void OnSelectMission(int index)
	{
		CurrentMission.currentMission = missions[index];
		sceneLoader.SwitchScene("MissionMode");
	}
}
